using System;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Requirements
{
    /// <summary>
    /// Denotes, that host must provide <see cref="IVostokHostingEnvironment.HostExtensions"/> with <see cref="Type"/> type to the application.
    /// </summary>
    [PublicAPI]
    public class VostokHostExtensionRequiredAttribute : Attribute
    {
        public Type Type;

        public VostokHostExtensionRequiredAttribute(Type type) =>
            Type = type;
    }
}