using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.HostExtensions
{
    [PublicAPI]
    public interface IVostokHostInfo
    {
        string Name { get; }
    }
}