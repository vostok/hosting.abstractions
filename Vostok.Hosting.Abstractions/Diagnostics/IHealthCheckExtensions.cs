using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Diagnostics
{
    [PublicAPI]
    public static class IHealthCheckExtensions
    {
        [ItemNotNull]
        public static async Task<HealthCheckResult> CheckSafeAsync([NotNull] this IHealthCheck check, CancellationToken cancellationToken)
        {
            try
            {
                return await check.CheckAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception error)
            {
                return HealthCheckResult.Failing($"Exception of type '{error.GetType().Name} has occurred: '{error.Message}'.");
            }
        }
    }
}
