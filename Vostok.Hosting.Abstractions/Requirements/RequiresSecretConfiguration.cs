using System;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Requirements
{
    /// <summary>
    /// Denotes, that host must setup <see cref="IVostokHostingEnvironment.SecretConfigurationSource"/> for <see cref="Type"/> type into <see cref="IVostokHostingEnvironment.ConfigurationProvider"/>.
    /// </summary>
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Class)]
    public class RequiresSecretConfiguration : Attribute
    {
        public Type Type;

        public RequiresSecretConfiguration(Type type) =>
            Type = type;
    }
}