using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.HostExtensions
{
    // CR(iloktionov): 1. xml docs everywhere
    // CR(iloktionov): 2. extension --> property of IVostokHostingEnvironment
    [PublicAPI]
    public interface IVostokApplicationLimits
    {
        // CR(iloktionov): Nullable (if no limits)?
        float Cpu { get; }

        // CR(iloktionov): Convert these two to a single memory limit
        long WorkingSet { get; }

        long PrivateBytes { get; }
    }
}