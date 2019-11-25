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
        public readonly Type Type;

        public readonly string Scope;

        public RequiresSecretConfiguration([NotNull] Type type, [CanBeNull] string scope = null)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Scope = scope;
        }
    }
}