using System.Threading;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Helpers
{
    /// <summary>
    /// <see cref="IVostokHostShutdown" /> allows the application to initiate graceful shutdown.
    /// </summary>
    [PublicAPI]
    public interface IVostokHostShutdown
    {
        bool IsInitiated { get; }

        void Initiate();
    }
}