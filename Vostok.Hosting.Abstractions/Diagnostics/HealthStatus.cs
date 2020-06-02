using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Diagnostics
{
    [PublicAPI]
    public enum HealthStatus
    {
        /// <summary>
        /// Application is doing well, nothing to worry about.
        /// </summary>
        Healthy,
        
        /// <summary>
        /// Application is able to perform its core functions, albeit with potentially degraded quality.
        /// </summary>
        Degraded,
        
        /// <summary>
        /// Application is not capable of performing some or all of its core, critical functions.
        /// </summary>
        Failing
    }
}
