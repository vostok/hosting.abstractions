using System;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Requirements
{
    /// <summary>
    /// Denotes, that host must setup <see cref="IVostokHostingEnvironment.ConfigurationSource"/> for <see cref="Type"/> type into <see cref="IVostokHostingEnvironment.ConfigurationProvider"/>.
    /// </summary>
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Class)]
    public class RequiresConfiguration : Attribute
    {
        public Type Type;

        public RequiresConfiguration(Type type) =>
            Type = type;
    }
}