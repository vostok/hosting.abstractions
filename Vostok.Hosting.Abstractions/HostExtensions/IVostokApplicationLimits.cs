using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.HostExtensions
{
    [PublicAPI]
    public interface IVostokApplicationLimits
    {
        float Cpu { get; }

        long WorkingSet { get; }

        long PrivateBytes { get; }
    }
}