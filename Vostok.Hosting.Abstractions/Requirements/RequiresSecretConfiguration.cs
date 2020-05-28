using System;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Requirements
{
    /// <summary>
    /// <para>Denotes that host must set up <see cref="IVostokHostingEnvironment.SecretConfigurationSource"/> for settings of type <see cref="Type"/> in <see cref="IVostokHostingEnvironment.ConfigurationProvider"/>.</para>
    /// <para>Optionally, <see cref="IVostokHostingEnvironment.SecretConfigurationSource"/> can also be scoped into given <see cref="Scope"/>.</para>
    /// </summary>
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
    public class RequiresSecretConfiguration : Requirement
    {
        [NotNull]
        public readonly Type Type;

        [NotNull]
        public readonly string[] Scope;

        public RequiresSecretConfiguration([NotNull] Type type, [NotNull] params string[] scope)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
        }
    }
}