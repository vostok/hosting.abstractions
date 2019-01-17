using JetBrains.Annotations;
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
        /// A log instance to be used for all logging from inside the hosted application.
        /// </summary>
        [NotNull]
        ILog Log { get; }
        
        /// <summary>
        /// A Hercules client instance to be used for all hercules events from inside the hosted application.
        /// </summary>
        [NotNull]
        IHerculesSink HerculesSink { get; }
    }
}