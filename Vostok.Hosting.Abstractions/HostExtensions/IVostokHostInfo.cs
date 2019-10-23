using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.HostExtensions
{
    // CR(iloktionov): unclear purpose. Remove?
    [PublicAPI]
    public interface IVostokHostInfo
    {
        string Name { get; }
    }
}