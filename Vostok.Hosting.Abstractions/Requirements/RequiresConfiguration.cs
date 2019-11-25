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
        public readonly Type Type;

        public readonly string Scope;

        public RequiresConfiguration([NotNull] Type type, [CanBeNull] string scope = null)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Scope = scope;
        }
    }
}