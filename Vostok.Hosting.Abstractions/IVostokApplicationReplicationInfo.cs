using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions
{
    /// <see cref="IVostokHostingEnvironment.ApplicationReplicationInfo"/>
    [PublicAPI]
    public interface IVostokApplicationReplicationInfo
    {
        /// <summary>
        /// <para>Zero-based index of application instance.</para>
        /// <para>This property may be updated during the application lifecycle.</para>
        /// </summary>
        int InstanceIndex { get; }

        /// <summary>
        /// <para>Total number of application instances.</para>
        /// <para>This property may be updated during the application lifecycle.</para>
        /// </summary>
        int InstancesCount { get; }
    }
}