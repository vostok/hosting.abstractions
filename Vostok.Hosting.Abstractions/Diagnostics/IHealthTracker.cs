using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Diagnostics
{
    /// <inheritdoc cref="IVostokApplicationDiagnostics.HealthTracker"/>
    [PublicAPI]
    public interface IHealthTracker
    {
        /// <summary>
        /// Returns current application health status.
        /// </summary>
        HealthStatus CurrentStatus { get; }

        /// <summary>
        /// Returns an observable that produces a new element each time <see cref="CurrentStatus"/> changes.
        /// </summary>
        [NotNull]
        IObservable<HealthStatus> ObserveStatus();

        /// <inheritdoc cref="ObserveStatus"/>
        [NotNull]
        IObservable<(HealthStatus previous, HealthStatus current)> ObserveStatusChanges();

        /// <summary>
        /// <para>Registers given info <paramref name="check"/> under given <paramref name="name"/> so that it can be queried.</para>
        /// <para>All registered checks are quired periodically to update application health status.</para>
        /// <para>Dispose of the returned result to cancel the registration and remove the check from use.</para>
        /// </summary>
        [NotNull]
        IDisposable RegisterCheck([NotNull] string name, [NotNull] IHealthCheck check);

        /// <summary>
        /// <para>Returns a detailed report of application health based on all registered checks.</para>
        /// <para>Triggers a synchronous round of checks if <paramref name="forceChecks"/> is <c>true</c>.</para>
        /// <para>May serve cached information if <paramref name="forceChecks"/> is <c>false</c>.</para>
        /// </summary>
        [NotNull]
        Task<HealthReport> ObtainReportAsync(bool forceChecks);
    }
}
