using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.HostExtensions
{
    // CR(iloktionov): sharding --> replication
    [PublicAPI]
    public interface IVostokApplicationShardingSettings
    {
        int Index { get; }

        int Count { get; } 
    }
}