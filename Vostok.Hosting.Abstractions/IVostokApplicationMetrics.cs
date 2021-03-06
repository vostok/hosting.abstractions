﻿using JetBrains.Annotations;
using Vostok.Metrics;

namespace Vostok.Hosting.Abstractions
{
    /// <inheritdoc cref="IVostokHostingEnvironment.Metrics"/>
    [PublicAPI]
    public interface IVostokApplicationMetrics
    {
        /// <summary>
        /// A metric context with no preconfigured tags.
        /// </summary>
        [NotNull]
        IMetricContext Root { get; }

        /// <summary>
        /// <para>A metric context preconfigured with tags from following <see cref="IVostokHostingEnvironment.ApplicationIdentity"/> properties (listed in order):</para>
        /// <list type="bullet">
        ///     <item><description><see cref="IVostokApplicationIdentity.Project"/></description></item>
        /// </list>
        /// </summary>
        [NotNull]
        IMetricContext Project { get; }

        /// <summary>
        /// <para>A metric context preconfigured with tags from following <see cref="IVostokHostingEnvironment.ApplicationIdentity"/> properties (listed in order):</para>
        /// <list type="bullet">
        ///     <item><description><see cref="IVostokApplicationIdentity.Project"/></description></item>
        ///     <item><description><see cref="IVostokApplicationIdentity.Subproject"/> (if specified)</description></item>
        /// </list>
        /// </summary>
        [NotNull]
        IMetricContext Subproject { get; }

        /// <summary>
        /// <para>A metric context preconfigured with tags from following <see cref="IVostokHostingEnvironment.ApplicationIdentity"/> properties (listed in order):</para>
        /// <list type="bullet">
        ///     <item><description><see cref="IVostokApplicationIdentity.Project"/></description></item>
        ///     <item><description><see cref="IVostokApplicationIdentity.Subproject"/> (if specified)</description></item>
        ///     <item><description><see cref="IVostokApplicationIdentity.Environment"/></description></item>
        /// </list>
        /// </summary>
        [NotNull]
        IMetricContext Environment { get; }

        /// <summary>
        /// <para>A metric context preconfigured with tags from following <see cref="IVostokHostingEnvironment.ApplicationIdentity"/> properties (listed in order):</para>
        /// <list type="bullet">
        ///     <item><description><see cref="IVostokApplicationIdentity.Project"/></description></item>
        ///     <item><description><see cref="IVostokApplicationIdentity.Subproject"/> (if specified)</description></item>
        ///     <item><description><see cref="IVostokApplicationIdentity.Environment"/></description></item>
        ///     <item><description><see cref="IVostokApplicationIdentity.Application"/></description></item>
        /// </list>
        /// </summary>
        [NotNull]
        IMetricContext Application { get; }

        /// <summary>
        /// <para>A metric context preconfigured with tags from following <see cref="IVostokHostingEnvironment.ApplicationIdentity"/> properties (listed in order):</para>
        /// <list type="bullet">
        ///     <item><description><see cref="IVostokApplicationIdentity.Project"/></description></item>
        ///     <item><description><see cref="IVostokApplicationIdentity.Subproject"/> (if specified)</description></item>
        ///     <item><description><see cref="IVostokApplicationIdentity.Environment"/></description></item>
        ///     <item><description><see cref="IVostokApplicationIdentity.Application"/></description></item>
        ///     <item><description><see cref="IVostokApplicationIdentity.Instance"/></description></item>
        /// </list>
        /// </summary>
        [NotNull]
        IMetricContext Instance { get; }
    }
}