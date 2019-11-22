using System;
using JetBrains.Annotations;
using Vostok.Configuration.Abstractions;

namespace Vostok.Hosting.Abstractions.Requirements
{
    /// <summary>
    /// Denotes, that host must setup <see cref="IConfigurationSource"/> for <see cref="Type"/> type at <see cref="IVostokHostingEnvironment.ConfigurationProvider"/>.
    /// </summary>
    [PublicAPI]
    public class VostokHostConfigurationProviderTypeRequiredAttribute : Attribute
    {
        public Type Type;

        public VostokHostConfigurationProviderTypeRequiredAttribute(Type type) =>
            Type = type;
    }
}