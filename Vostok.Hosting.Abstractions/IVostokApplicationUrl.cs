using System;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions
{
    [PublicAPI]
    public class IVostokApplicationUrl
    {
        public Uri Url { get; }
    }
}