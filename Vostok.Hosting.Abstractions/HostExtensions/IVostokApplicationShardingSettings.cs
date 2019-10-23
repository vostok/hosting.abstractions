using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.HostExtensions
{
    [PublicAPI]
    public interface IVostokApplicationShardingSettings
    {
        int Index { get; }

        int Count { get; }
    }
}