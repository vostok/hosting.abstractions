using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions
{
    [PublicAPI]
    public class IVostokApplicationPort
    {
        public int Port { get; }
    }
}