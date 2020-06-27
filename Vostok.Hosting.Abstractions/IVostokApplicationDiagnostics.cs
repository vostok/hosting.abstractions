using JetBrains.Annotations;
using Vostok.Hosting.Abstractions.Diagnostics;

namespace Vostok.Hosting.Abstractions
{
    /// <inheritdoc cref="IVostokHostingEnvironment.Diagnostics"/>
    [PublicAPI]
    public interface IVostokApplicationDiagnostics
    {
        /// <summary>
        /// <see cref="IDiagnosticInfo"/> allows to register arbitrary diagnostic info providers and query them.
        /// </summary>
        [NotNull]
        IDiagnosticInfo Info { get; }

        /// <summary>
        /// <see cref="IHealthTracker"/> provides a simple health check infrastucture and allows to track internal application health.
        /// </summary>
        [NotNull]
        IHealthTracker HealthTracker { get; }
    }
}
