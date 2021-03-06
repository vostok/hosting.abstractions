﻿using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions
{
    /// <inheritdoc cref="IVostokHostingEnvironment.ApplicationLimits"/>
    [PublicAPI]
    public interface IVostokApplicationLimits
    {
        /// <summary>
        /// <para>Number of maximum allowed cpu usage in cpu units.</para>
        /// <para>One unit is equivalent to 1 cpu core.</para>
        /// <para>Fractional values are allowed.</para>
        /// <para>A <c>null</c> value denotes no limit.</para>
        /// <para>If limit has been changed, new value will be returned.</para>
        /// </summary>
        float? CpuUnits { get; }

        /// <summary>
        /// <para>Number of maximum allowed memory usage in bytes.</para>
        /// <para>A <c>null</c> value denotes no limit.</para>
        /// <para>If limit has been changed, new value will be returned.</para>
        /// </summary>
        long? MemoryBytes { get; }
    }
}