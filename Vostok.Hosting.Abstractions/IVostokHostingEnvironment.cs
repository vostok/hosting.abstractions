using JetBrains.Annotations;
using Vostok.Logging.Abstractions;

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
    }
}