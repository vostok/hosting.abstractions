using System;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Requirements
{
    /// <summary>
    /// Denotes, that host must provide <see cref="IVostokHostingEnvironment.HostExtensions"/> with <see cref="Type"/> type to the application.
    /// </summary>
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Class)]
    public class RequiresHostExtensionAttribute : Attribute
    {
        public readonly Type Type;

        public RequiresHostExtensionAttribute([NotNull] Type type) =>
            Type = type ?? throw new ArgumentNullException(nameof(type));
    }
}