using System;
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

namespace Vostok.Hosting.Abstractions
{
    [PublicAPI]
    public static class IVostokHostingEnvironmentExtensions
    {
        [NotNull]
        public static IVostokHostingEnvironment WithAdditionalShutdownToken(
            [NotNull] this IVostokHostingEnvironment environment, out CancellationTokenSource source)
            => WithAdditionalShutdownToken(environment, (source = new CancellationTokenSource()).Token);

        [NotNull]
        public static IVostokHostingEnvironment WithAdditionalShutdownToken(
            [NotNull] this IVostokHostingEnvironment environment, CancellationToken token)
        {
            var linkedSource = CancellationTokenSource.CreateLinkedTokenSource(environment.ShutdownToken, token);
            var linkedToken = linkedSource.Token;

            return new EnvironmentWithOverrides(environment)
            {
                CustomShutdownToken = linkedToken
            };
        }

        [NotNull]
        public static IVostokHostingEnvironment CustomizeLog(
            [NotNull] this IVostokHostingEnvironment environment, 
            [NotNull] Func<ILog, ILog> transform)
            => new EnvironmentWithOverrides(environment) { CustomLog = transform(environment.Log) };

        [NotNull]
        public static IVostokHostingEnvironment CustomizeConfigurationSource(
            [NotNull] this IVostokHostingEnvironment environment,
            [NotNull] Func<IConfigurationSource, IConfigurationSource> transform)
            => new EnvironmentWithOverrides(environment) { CustomConfigurationSource = transform(environment.ConfigurationSource) };

        [NotNull]
        public static IVostokHostingEnvironment CustomizeSecretConfigurationSource(
            [NotNull] this IVostokHostingEnvironment environment,
            [NotNull] Func<IConfigurationSource, IConfigurationSource> transform)
            => new EnvironmentWithOverrides(environment) { CustomSecretConfigurationSource = transform(environment.SecretConfigurationSource) };


        private class EnvironmentWithOverrides : IVostokHostingEnvironment
        {
            private readonly IVostokHostingEnvironment environment;

            public EnvironmentWithOverrides(IVostokHostingEnvironment environment)
                => this.environment = environment;

            [CanBeNull]
            public CancellationToken? CustomShutdownToken { get; set; }

            [CanBeNull]
            public ILog CustomLog { get; set; }

            [CanBeNull]
            public IConfigurationSource CustomConfigurationSource{ get; set; }

            [CanBeNull]
            public IConfigurationSource CustomSecretConfigurationSource { get; set; }

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

            public IVostokApplicationDiagnostics Diagnostics
                => environment.Diagnostics;

            public ILog Log
                => CustomLog ?? environment.Log;

            public ITracer Tracer
                => environment.Tracer;

            public IHerculesSink HerculesSink
                => environment.HerculesSink;

            public IConfigurationSource ConfigurationSource
                => CustomConfigurationSource ?? environment.ConfigurationSource;

            public IConfigurationProvider ConfigurationProvider
                => environment.ConfigurationProvider;

            public IConfigurationSource SecretConfigurationSource
                => CustomSecretConfigurationSource ?? environment.SecretConfigurationSource;

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
