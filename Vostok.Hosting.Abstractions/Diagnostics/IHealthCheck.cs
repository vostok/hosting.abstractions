using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Diagnostics
{
    [PublicAPI]
    public interface IHealthCheck
    {
        /// <summary>
        /// <para>Performs a check of a single particular aspect of application health.</para>
        /// <para>This method should not throw exceptions.</para>
        /// <para>This method should not cache its results (generally).</para>
        /// </summary>
        [ItemNotNull]
        Task<HealthCheckResult> CheckAsync(CancellationToken cancellationToken);
    }
}
