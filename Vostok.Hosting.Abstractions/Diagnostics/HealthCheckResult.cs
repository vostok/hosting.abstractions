using System;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Diagnostics
{
    [PublicAPI]
    public class HealthCheckResult
    {
        public HealthCheckResult(HealthStatus status, [CanBeNull] string reason)
        {
            Status = status;
            Reason = reason;
        }

        [NotNull]
        public static HealthCheckResult Healthy()
            => new HealthCheckResult(HealthStatus.Healthy, null);

        [NotNull]
        public static HealthCheckResult Degraded([NotNull] string reason)
            => new HealthCheckResult(HealthStatus.Degraded, reason ?? throw new ArgumentNullException(nameof(reason)));

        [NotNull]
        public static HealthCheckResult Failing([NotNull] string reason)
            => new HealthCheckResult(HealthStatus.Failing, reason ?? throw new ArgumentNullException(nameof(reason)));

        /// <summary>
        /// Current health status.
        /// </summary>
        public HealthStatus Status { get; }

        /// <summary>
        /// Reason for choosing <see cref="Status"/>.
        /// </summary>
        [CanBeNull]
        public string Reason { get; }
    }
}
