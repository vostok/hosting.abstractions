using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Diagnostics
{
    /// <inheritdoc cref="IVostokApplicationDiagnostics.HealthTracker"/>
    [PublicAPI]
    public interface IHealthTracker : IEnumerable<(string name, IHealthCheck check)>
    {
        /// <summary>
        /// Returns current application health status. May serve cached information.
        /// </summary>
        HealthStatus CurrentStatus { get; }

        /// <summary>
        /// Returns current application health report. May serve cached information.
        /// </summary>
        HealthReport CurrentReport { get; }

        /// <summary>
        /// Returns an observable that produces a new element each time <see cref="CurrentStatus"/> changes.
        /// </summary>
        [NotNull]
        IObservable<HealthStatus> ObserveStatus();

        /// <summary>
        /// Returns an observable that produces a new element each time a <see cref="HealthReport"/> is produced.
        /// </summary>
        [NotNull]
        IObservable<HealthReport> ObserveReports();

        /// <summary>
        /// Returns an observable that produces a new element each time <see cref="CurrentStatus"/> changes.
        /// </summary>
        [NotNull]
        IObservable<(HealthStatus previous, HealthStatus current)> ObserveStatusChanges();

        /// <summary>
        /// <para>Registers given info <paramref name="check"/> under given <paramref name="name"/> so that it can be queried.</para>
        /// <para>All registered checks are quired periodically to update application health status.</para>
        /// <para>Dispose of the returned result to cancel the registration and remove the check from use.</para>
        /// </summary>
        [NotNull]
        IDisposable RegisterCheck([NotNull] string name, [NotNull] IHealthCheck check);
    }
}
