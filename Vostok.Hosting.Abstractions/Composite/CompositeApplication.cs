using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Vostok.Logging.Abstractions;

namespace Vostok.Hosting.Abstractions.Composite
{
    /// <summary>
    /// <para><see cref="CompositeApplication"/> allows to combine multiple instances of <see cref="IVostokApplication"/> into one.</para>
    /// <para>Its main use case is to simplify development of apps that consist of several loosely coupled components (such as HTTP API + periodical background jobs).</para>
    /// </summary>
    [PublicAPI]
    public class CompositeApplication : IVostokApplication, IDisposable
    {
        private readonly IReadOnlyList<IVostokApplication> applications;
        private readonly CompositeApplicationSettings settings;

        private volatile IVostokHostingEnvironment environment;
        private volatile CancellationTokenSource localShutdown;

        public CompositeApplication([NotNull] Action<ICompositeApplicationBuilder> configure)
            => (applications, settings) = CompositeApplicationBuilder.Build(configure);

        /// <summary>
        /// <para>Override this method to perform initialization before subapplications get initialized.</para>
        /// <para>For instance, it's a suitable place to set up a shared DI container and save it in given <paramref name="vostok"/>'s <see cref="IVostokHostingEnvironment.HostExtensions"/> for subapplications to fetch later.</para>
        /// </summary>
        public virtual Task PreInitializeAsync(IVostokHostingEnvironment externalEnvironment)
            => Task.CompletedTask;

        public async Task InitializeAsync(IVostokHostingEnvironment externalEnvironment)
        {
            environment = externalEnvironment.WithAdditionalShutdownToken(out localShutdown);

            await PreInitializeAsync(environment);

            await (settings.UseParallelInitialization
                ? ExecuteInParallel(SelectInitializeMethods())
                : ExecuteSequentially(SelectInitializeMethods()));
        }

        public Task RunAsync(IVostokHostingEnvironment vostok)
            => settings.UseParallelExecution
                ? ExecuteInParallel(SelectRunMethods())
                : ExecuteSequentially(SelectRunMethods());

        public virtual void Dispose()
            => Task.WhenAll(applications.OfType<IDisposable>().Select(app => Task.Run(() => app.Dispose())))
                .GetAwaiter()
                .GetResult();

        internal IEnumerable<Type> ApplicationTypes => applications.Select(app => app.GetType());

        private async Task ExecuteSequentially(IEnumerable<Func<IVostokHostingEnvironment, Task>> actions)
        {
            foreach (var action in actions)
            {
                environment.ShutdownToken.ThrowIfCancellationRequested();

                await action(environment);
            }
        }

        private async Task ExecuteInParallel(IEnumerable<Func<IVostokHostingEnvironment, Task>> actions)
        {
            var tasks = actions.Select(action => Task.Run(() => action(environment))).ToList();

            var errors = new List<Exception>();

            while (tasks.Any())
            {
                var completedTask = await Task.WhenAny(tasks);

                tasks.Remove(completedTask);

                try
                {
                    await completedTask;
                }
                catch (OperationCanceledException)
                {
                }
                catch (Exception error)
                {
                    errors.Add(error);

                    environment.Log.ForContext<CompositeApplication>().Error(error, "One of the subapplications has crashed. Shutting down others..");

                    if (!localShutdown.IsCancellationRequested)
                        localShutdown.Cancel();
                }
            }

            switch (errors.Count)
            {
                case 0: return;
                case 1: ExceptionDispatchInfo.Capture(errors.Single()).Throw(); 
                    break;
                default: throw new AggregateException(errors);
            }
        }

        private IEnumerable<Func<IVostokHostingEnvironment, Task>> SelectInitializeMethods()
            => applications.Select<IVostokApplication, Func<IVostokHostingEnvironment, Task>>(app => app.InitializeAsync);

        private IEnumerable<Func<IVostokHostingEnvironment, Task>> SelectRunMethods()
            => applications.Select<IVostokApplication, Func<IVostokHostingEnvironment, Task>>(app => app.RunAsync);
    }
}
