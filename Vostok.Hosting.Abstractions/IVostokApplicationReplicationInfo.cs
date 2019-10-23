using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions
{
    /// <see cref="IVostokHostingEnvironment.ApplicationReplicationInfo"/>
    [PublicAPI]
    public interface IVostokApplicationReplicationInfo
    {
        /// <summary>
        /// <para>Zero-based index of application instance.</para>
        /// </summary>
        int ReplicaIndex { get; }

        /// <summary>
        /// <para>Total number of application instances.</para>
        /// </summary>
        int ReplicasCount { get; }
    }
}