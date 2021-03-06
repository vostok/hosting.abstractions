﻿using System;
using System.Threading;
using JetBrains.Annotations;
using Vostok.ClusterConfig.Client.Abstractions;
using Vostok.Configuration.Abstractions;
using Vostok.Context;
using Vostok.Datacenters;
using Vostok.Hercules.Client.Abstractions;
using Vostok.Hosting.Abstractions.Requirements;
using Vostok.Logging.Abstractions;
using Vostok.ServiceDiscovery.Abstractions;
using Vostok.Tracing.Abstractions;

namespace Vostok.Hosting.Abstractions
{
    /// <summary>
    /// <para><see cref="IVostokHostingEnvironment"/> is the interface between the application (see <see cref="IVostokApplication"/>) and the host responsible for launching it.</para>
    /// <para>It provides a set of universal components (logging, configuration, metrics, tracing, etc.) common to all Vostok applications.</para>
    /// <para>It also contains an extension point to accomodate host-specific features.</para>
    /// </summary>
    [PublicAPI]
    public interface IVostokHostingEnvironment
    {
        /// <summary>
        /// <para>A token that can be set by the host to indicate an intentional application shutdown that is about to happen.</para>
        /// <para>Applications are expected to terminate gracefully and return from <see cref="IVostokApplication.RunAsync"/> upon receiving such notification.</para>
        /// <para>Hosts typically have a timeout for graceful shutdown (see <see cref="ShutdownTimeout"/>): if the application fails to stop in a reasonable amount of time, it will get terminated forcefully.</para>
        /// </summary>
        CancellationToken ShutdownToken { get; }

        /// <summary>
        /// <para>Amount of time the host will wait for the application to shut down gracefully after signaling <see cref="ShutdownToken"/>.</para>
        /// </summary>
        TimeSpan ShutdownTimeout { get; }

        /// <summary>
        /// <para>A set of properties that allow to uniquely identify an instance of the application.</para>
        /// <para>See <see cref="IVostokApplicationIdentity.Project"/>, <see cref="IVostokApplicationIdentity.Environment"/> and <see cref="IVostokApplicationIdentity.Instance"/> for more details.</para>
        /// </summary>
        [NotNull]
        IVostokApplicationIdentity ApplicationIdentity { get; }

        /// <summary>
        /// <para>A set of properties that denotes application cpu and memory usage limits.</para>
        /// </summary>
        [NotNull]
        IVostokApplicationLimits ApplicationLimits { get; }

        /// <summary>
        /// <para>A set of properties that denotes application replication factor and index of current instance.</para>
        /// <para>If replication has been changed, new instance will be returned.</para>
        /// </summary>
        [NotNull]
        IVostokApplicationReplicationInfo ApplicationReplicationInfo { get; }

        /// <summary>
        /// <para>A set of scoped metric contexts corresponding to different levels of <see cref="ApplicationIdentity"/>.</para>
        /// <para>See <see cref="IVostokApplicationMetrics.Project"/>, <see cref="IVostokApplicationMetrics.Subproject"/>, <see cref="IVostokApplicationMetrics.Environment"/>, <see cref="IVostokApplicationMetrics.Application"/> and <see cref="IVostokApplicationMetrics.Instance"/> for more details.</para>
        /// </summary>
        [NotNull]
        IVostokApplicationMetrics Metrics { get; }

        /// <summary>
        /// <para>A log instance to be used for all logging from inside the hosted application.</para>
        /// <para>It typically comes set up to write logs to both local files and Hercules.</para>
        /// </summary>
        [NotNull]
        ILog Log { get; }

        /// <summary>
        /// <para>A tracer instance used to instrument the application with distributed tracing.</para>
        /// <para>It typically comes set up to send produced spans to Hercules with <see cref="HerculesSink"/>.</para>
        /// </summary>
        [NotNull]
        ITracer Tracer { get; }

        /// <summary>
        /// An instance of Hercules client that can be used to send arbitrary user-defined events to Hercules.
        /// </summary>
        [NotNull]
        IHerculesSink HerculesSink { get; }

        /// <summary>
        /// <para>A source of raw configuration parameters provided by the host system.</para>
        /// <para>Use it in conjunction with <see cref="ConfigurationProvider"/>.</para>
        /// <para>When working with secret settings, use <see cref="SecretConfigurationSource"/> instead.</para>
        /// </summary>
        [NotNull]
        IConfigurationSource ConfigurationSource { get; }

        /// <summary>
        /// <para>A configuration provider preconfigured by the host system.</para>
        /// <para>Use it to obtain settings from built-in <see cref="ConfigurationSource"/> or custom <see cref="IConfigurationSource"/>s.</para>
        /// <para>When working with secret settings, use <see cref="SecretConfigurationProvider"/> instead.</para>
        /// </summary>
        [NotNull]
        IConfigurationProvider ConfigurationProvider { get; }

        /// <summary>
        /// <para>A source of raw secret configuration parameters provided by the host system.</para>
        /// <para>Use it in conjunction with <see cref="ConfigurationProvider"/>.</para>
        /// </summary>
        [NotNull]
        IConfigurationSource SecretConfigurationSource { get; }

        /// <summary>
        /// <para>A configuration provider for secret settings preconfigured by the host system.</para>
        /// <para>Use it to obtain settings from built-in <see cref="SecretConfigurationSource"/> or custom secret <see cref="IConfigurationSource"/>s.</para>
        /// </summary>
        [NotNull]
        IConfigurationProvider SecretConfigurationProvider { get; }

        /// <summary>
        /// <para>Client for ClusterConfig service used to obtain application settings.</para>
        /// <para>Use it in conjunction with <see cref="ConfigurationProvider"/> and <see cref="IConfigurationSource"/>.</para>
        /// </summary>
        [NotNull]
        IClusterConfigClient ClusterConfigClient { get; }

        /// <summary>
        /// <para>A server-side service discovery tool used to add register/unregister application replica in common registry.</para>
        /// <para>This <see cref="IServiceBeacon"/> instance is typically manipulated by the host and does not require manual handling.</para>
        /// <para>See <see cref="IServiceBeacon"/> for more details.</para>
        /// </summary>
        [NotNull]
        IServiceBeacon ServiceBeacon { get; }

        /// <summary>
        /// <para>A port provided by host.</para>
        /// <para>Mark <see cref="IVostokApplication"/> with <see cref="RequiresPort"/> if you need it.</para>
        /// <para>Same value will be used in <see cref="ServiceBeacon"/>.</para>
        /// </summary>
        [CanBeNull]
        int? Port { get; }

        /// <summary>
        /// <para>A client-side service discovery provider required for inter-service communication.</para>
        /// <para>See <see cref="IServiceLocator.Locate"/> for more details.</para>
        /// </summary>
        [NotNull]
        IServiceLocator ServiceLocator { get; }

        /// <summary>
        /// Mutable type-based ambient context properties.
        /// </summary>
        [NotNull]
        IContextGlobals ContextGlobals { get; }

        /// <summary>
        /// Mutable name-based ambient context properties.
        /// </summary>
        [NotNull]
        IContextProperties ContextProperties { get; }

        /// <summary>
        /// Ambient context configuration.
        /// </summary>
        [NotNull]
        IContextConfiguration ContextConfiguration { get; }

        /// <summary>
        /// A helper assisting in mapping machine names and addresses to datacenters.
        /// </summary>
        [NotNull]
        IDatacenters Datacenters { get; }

        /// <summary>
        /// An extension point for arbitrary services provided by the host.
        /// </summary>
        [NotNull]
        IVostokHostExtensions HostExtensions { get; }
    }
}