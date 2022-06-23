using JetBrains.Annotations;
using Vostok.Hosting.Abstractions.Diagnostics;

namespace Vostok.Hosting.Abstractions
{
    /// <summary>
    /// A set of tools which enables user to track and observe application health state.
    /// </summary>
    [PublicAPI]
    public interface IVostokApplicationDiagnostics
    {
        /// <summary>
        /// <see cref="IDiagnosticInfo"/> allows to register arbitrary diagnostic info providers and query them.
        /// </summary>
        [NotNull]
        IDiagnosticInfo Info { get; }

        /// <summary>
        /// <see cref="IHealthTracker"/> provides a simple health check infrastructure and allows to track internal application health.
        /// </summary>
        [NotNull]
        IHealthTracker HealthTracker { get; }
    }
}