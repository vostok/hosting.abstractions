using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions
{
    /// <summary>
    /// <para><see cref="IVostokApplication"/> is the interface developers implement in order to create Vostok-compatible services.</para>
    /// <para>It represents a hosted application. A host launching the application is responsible for doing the following:</para>
    /// <list type="bullet">
    ///     <item><description>Providing an implementation of <see cref="IVostokHostingEnvironment"/>.</description></item>
    ///     <item><description>Running the application by calling <see cref="InitializeAsync"/> and then <see cref="RunAsync"/>.</description></item>
    /// </list>
    /// </summary>
    [PublicAPI]
    public interface IVostokApplication
    {
        IReadOnlyList<IVostokApplicationRequirement> Requirements { get; }

        /// <summary>
        /// <para>Performs any initialization or warm-up required for successful startup.</para>
        /// <para>It is assumed that the application is ready to run after returning control from this method.</para>
        /// <para><see cref="InitializeAsync"/> is guaranteed to be called once before <see cref="RunAsync"/>.</para>
        /// </summary>
        /// <param name="environment">Environment context provided by the host.</param>
        [NotNull]
        Task InitializeAsync([NotNull] IVostokHostingEnvironment environment);

        /// <summary>
        /// <para>Runs the application until it either crashes with an exception or deliberately decides to terminate.</para>
        /// <para>This method should not return control until the application has finished working and is ready to be terminated.</para>
        /// <para>This method is also expected to terminate gracefully when provided <see cref="IVostokHostingEnvironment.ShutdownToken"/> is set by the host.</para>
        /// <para><see cref="InitializeAsync"/> is guaranteed to be called once before <see cref="RunAsync"/> with the same instance of <paramref name="environment"/>.</para>
        /// </summary>
        /// <param name="environment">Environment context provided by the host.</param>
        [NotNull]
        Task RunAsync([NotNull] IVostokHostingEnvironment environment);
    }
}