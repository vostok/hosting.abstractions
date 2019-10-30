using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions
{
    /// <inheritdoc cref="IVostokHostingEnvironment.ApplicationReplicationInfo"/>
    [PublicAPI]
    public interface IVostokApplicationReplicationInfo
    {
        /// <summary>
        /// <para>Zero-based index of application instance.</para>
        /// </summary>
        int InstanceIndex { get; }

        /// <summary>
        /// <para>Total number of application instances.</para>
        /// </summary>
        int InstancesCount { get; }
    }
}