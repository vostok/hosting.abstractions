﻿using System;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Requirements
{
    /// <summary>
    /// <para>Denotes that host must set up <see cref="IVostokHostingEnvironment.ConfigurationSource"/> merged with <see cref="IVostokHostingEnvironment.SecretConfigurationSource"/> for settings of type <see cref="Type"/> in <see cref="IVostokHostingEnvironment.ConfigurationProvider"/>.</para>
    /// <para>Optionally, merged source can also be scoped into given <see cref="Scope"/>.</para>
    /// </summary>
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
    public class RequiresMergedConfiguration : Attribute
    {
        [NotNull]
        public readonly Type Type;

        [NotNull]
        public readonly string[] Scope;

        public RequiresMergedConfiguration([NotNull] Type type, [NotNull] params string[] scope)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
        }
    }
}