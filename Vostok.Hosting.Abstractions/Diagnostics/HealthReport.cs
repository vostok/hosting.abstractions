using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Diagnostics
{
    /// <summary>
    /// <see cref="HealthReport"/> provides a detailed report of application health.
    /// </summary>
    [PublicAPI]
    public class HealthReport
    {
        public HealthReport(HealthStatus status, [NotNull] IReadOnlyDictionary<string, HealthCheckResult> checks)
        {
            Status = status;
            Checks = checks ?? throw new ArgumentNullException(nameof(checks));
        }

        /// <summary>
        /// Current application-wide health status.
        /// </summary>
        public HealthStatus Status { get; }

        /// <summary>
        /// Breakdown of recent health check results.
        /// </summary>
        [NotNull]
        public IReadOnlyDictionary<string, HealthCheckResult> Checks { get; }

        /// <summary>
        /// Returns all reported reasons for statuses other than <see cref="HealthStatus.Healthy"/>.
        /// </summary>
        [NotNull]
        public IEnumerable<string> Problems => Checks.Values
            .Where(result => result.Status != HealthStatus.Healthy)
            .Where(result => result.Reason != null)
            .Select(result => result.Reason);
    }
}
