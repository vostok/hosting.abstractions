using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Vostok.Hosting.Abstractions.Helpers;
using Vostok.Logging.Abstractions;

namespace Vostok.Hosting.Abstractions.Composite
{
    /// <summary>
    /// <para><see cref="CompositeApplication"/> allows to combine multiple instances of <see cref="IVostokApplication"/> into one.</para>
    /// <para>Its main use case is to simplify development of apps that consist of several loosely coupled components (such as HTTP API + periodical background jobs).</para>
    /// </summary>
    [PublicAPI]
    public class CompositeApplication : IVostokApplication
    {
        private readonly IReadOnlyList<IVostokApplication> applications;
        private readonly CompositeApplicationSettings settings;

        public CompositeApplication([NotNull] Action<ICompositeApplicationBuilder> configure)
            => (applications, settings) = CompositeApplicationBuilder.Build(configure);

        public virtual Task InitializeAsync(IVostokHostingEnvironment environment) 
            => settings.UseParallelInitialization
                ? ExecuteInParallel(environment, SelectInitializeMethods())
                : ExecuteSequentially(environment, SelectInitializeMethods());

        public virtual Task RunAsync(IVostokHostingEnvironment environment)
            => settings.UseParallelExecution
                ? ExecuteInParallel(environment, SelectRunMethods())
                : ExecuteSequentially(environment, SelectRunMethods());

        internal IEnumerable<Type> ApplicationTypes => applications.Select(app => app.GetType());

        private async Task ExecuteSequentially(IVostokHostingEnvironment environment, IEnumerable<Func<IVostokHostingEnvironment, Task>> actions)
        {
            foreach (var action in actions)
                await action(environment).ConfigureAwait(false);
        }

        private async Task ExecuteInParallel(IVostokHostingEnvironment environment, IEnumerable<Func<IVostokHostingEnvironment, Task>> actions)
        {
            environment = environment.WithAdditionalShutdownToken(out var localShutdown);

            var tasks = actions.Select(action => Task.Run(() => action(environment))).ToList();

            var errors = new List<Exception>();

            while (tasks.Any())
            {
                var completedTask = await Task.WhenAny(tasks).ConfigureAwait(false);

                tasks.Remove(completedTask);

                try
                {
                    await completedTask.ConfigureAwait(false);
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
                case 1: throw errors.Single();
                default: throw new AggregateException(errors);
            }
        }

        private IEnumerable<Func<IVostokHostingEnvironment, Task>> SelectInitializeMethods()
            => applications.Select<IVostokApplication, Func<IVostokHostingEnvironment, Task>>(app => app.InitializeAsync);

        private IEnumerable<Func<IVostokHostingEnvironment, Task>> SelectRunMethods()
            => applications.Select<IVostokApplication, Func<IVostokHostingEnvironment, Task>>(app => app.RunAsync);
    }
}
