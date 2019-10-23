using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions
{
    /// <inheritdoc cref="IVostokHostingEnvironment.ApplicationLimits"/>
    [PublicAPI]
    public interface IVostokApplicationLimits
    {
        /// <summary>
        /// <para>Number of maximum allowed cpu usage in cpu units.</para>
        /// <para>One unit is equivalent to 1 cpu core.</para>
        /// <para>Fractional requests are allowed.</para>
        /// <para>The <c>null</c> value denotes no limit.</para>
        /// </summary>
        float? CpuUnits { get; }

        /// <summary>
        /// <para>Number of maximum allowed memory usage in bytes.</para>
        /// <para>The <c>null</c> value denotes no limit.</para>
        /// </summary>
        long? MemoryBytes { get; }
    }
}