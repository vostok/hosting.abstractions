﻿using System;
using System.Threading;
using JetBrains.Annotations;
using Vostok.ClusterConfig.Client.Abstractions;
using Vostok.Configuration.Abstractions;
using Vostok.Context;
using Vostok.Datacenters;
using Vostok.Hercules.Client.Abstractions;
using Vostok.Logging.Abstractions;
using Vostok.ServiceDiscovery.Abstractions;
using Vostok.Tracing.Abstractions;

namespace Vostok.Hosting.Abstractions.Helpers
{
    internal static class IVostokHostingEnvironmentExtensions
    {
        [NotNull]
        public static IVostokHostingEnvironment WithAdditionalShutdownToken(
            [NotNull] this IVostokHostingEnvironment environment,
            out CancellationTokenSource source)
        {
            source = new CancellationTokenSource();

            var linkedSource = CancellationTokenSource.CreateLinkedTokenSource(environment.ShutdownToken, source.Token);
            var linkedToken = linkedSource.Token;

            return new EnvironmentWithOverrides(environment)
            {
                CustomShutdownToken = linkedToken
            };
        }

        private class EnvironmentWithOverrides : IVostokHostingEnvironment
        {
            private readonly IVostokHostingEnvironment environment;

            public EnvironmentWithOverrides(IVostokHostingEnvironment environment)
                => this.environment = environment;

            [CanBeNull]
            public CancellationToken? CustomShutdownToken { get; set; }

            public CancellationToken ShutdownToken
                => CustomShutdownToken ?? environment.ShutdownToken;

            public TimeSpan ShutdownTimeout
                => environment.ShutdownTimeout;

            public IVostokApplicationIdentity ApplicationIdentity
                => environment.ApplicationIdentity;

            public IVostokApplicationLimits ApplicationLimits
                => environment.ApplicationLimits;

            public IVostokApplicationReplicationInfo ApplicationReplicationInfo
                => environment.ApplicationReplicationInfo;

            public IVostokApplicationMetrics Metrics
                => environment.Metrics;

            public ILog Log
                => environment.Log;

            public ITracer Tracer
                => environment.Tracer;

            public IHerculesSink HerculesSink
                => environment.HerculesSink;

            public IConfigurationSource ConfigurationSource
                => environment.ConfigurationSource;

            public IConfigurationProvider ConfigurationProvider
                => environment.ConfigurationProvider;

            public IConfigurationSource SecretConfigurationSource
                => environment.SecretConfigurationSource;

            public IConfigurationProvider SecretConfigurationProvider
                => environment.SecretConfigurationProvider;

            public IClusterConfigClient ClusterConfigClient
                => environment.ClusterConfigClient;

            public IServiceBeacon ServiceBeacon
                => environment.ServiceBeacon;

            public int? Port
                => environment.Port;

            public IServiceLocator ServiceLocator
                => environment.ServiceLocator;

            public IContextGlobals ContextGlobals
                => environment.ContextGlobals;

            public IContextProperties ContextProperties
                => environment.ContextProperties;

            public IContextConfiguration ContextConfiguration
                => environment.ContextConfiguration;

            public IDatacenters Datacenters
                => environment.Datacenters;

            public IVostokHostExtensions HostExtensions
                => environment.HostExtensions;
        }
    }
}
