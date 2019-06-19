using System.Threading;
using JetBrains.Annotations;
using Vostok.Configuration.Abstractions;
using Vostok.Logging.Abstractions;
using Vostok.Hercules.Client.Abstractions;

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
        /// <para>Hosts typically have a timeout for graceful shutdown: if the application fails to stop in a reasonable amount of time, it will get terminated forcefully.</para>
        /// </summary>
        CancellationToken ShutdownToken { get; }

        /// <summary>
        /// <para>Identity is a set of properties that allow to uniquely identify an instance of the application.</para>
        /// <para>See <see cref="IVostokApplicationIdentity.Project"/>, <see cref="IVostokApplicationIdentity.Environment"/> and <see cref="IVostokApplicationIdentity.Instance"/> for more details.</para>
        /// </summary>
        [NotNull]
        IVostokApplicationIdentity ApplicationIdentity { get; }


        /// <summary>
        /// A log instance to be used for all logging from inside the hosted application.
        /// </summary>
        [NotNull]
        ILog Log { get; }
        
        /// <summary>
        /// A Hercules client instance to be used for all hercules events from inside the hosted application.
        /// </summary>
        [NotNull]
        IHerculesSink HerculesSink { get; }
        
        /// <summary>
        /// An IConfigurationProvider instance to obtain settings inside the hosted application.
        /// </summary>
        [NotNull]
        IConfigurationProvider ConfigurationProvider { get; }
        
        /// <summary>
        /// An IConfigurationSource instance which provides hosted application configuration in the form of raw settings trees.
        /// </summary>
        [NotNull]
        IConfigurationSource ConfigurationSource { get; }

    }
}