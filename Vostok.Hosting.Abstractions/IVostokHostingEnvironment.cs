using JetBrains.Annotations;
using Vostok.Configuration.Abstractions;
using Vostok.Logging.Abstractions;
using Vostok.Hercules.Client.Abstractions;

namespace Vostok.Hosting.Abstractions
{
    /// <summary>
    /// Everything required by a hosted application.
    /// </summary>
    [PublicAPI]
    public interface IVostokHostingEnvironment
    {
        /// <summary>
        /// <para>A set of properties that allow to uniquely identify an instance of the application.</para>
        /// <para>See <see cref="IVostokApplicationIdentity.Project"/>, <see cref="IVostokApplicationIdentity.Environment"/> and <see cref="IVostokApplicationIdentity.Instance"/> for more details.</para>
        /// </summary>
        [NotNull]
        IVostokApplicationIdentity ApplicationIdentity { get; }

        /// <summary>
        /// <para>A set of scoped metric contexts corresponding to different levels of <see cref="ApplicationIdentity"/>.</para>
        /// <para>See <see cref="IVostokApplicationMetrics.Project"/>, <see cref="IVostokApplicationMetrics.Environment"/>, <see cref="IVostokApplicationMetrics.Application"/>  and <see cref="IVostokApplicationMetrics.Instance"/> for more details.</para>
        /// </summary>
        [NotNull]
        IVostokApplicationMetrics Metrics { get; }

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