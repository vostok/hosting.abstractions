using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Requirements
{
    /// <summary>
    /// Provides helper methods to detect attribute-based requirements on application types, such as <see cref="RequiresConfiguration"/> or <see cref="RequiresHostExtension"/>.
    /// </summary>
    [PublicAPI]
    public static class RequirementDetector
    {
        public static bool RequiresPort(Type applicationType)
            => GetAttributes<RequiresPort>(applicationType).Any();

        public static IEnumerable<Type> GetRequiredHostExtensions(Type applicationType)
            => GetAttributes<RequiresHostExtension>(applicationType).Select(req => req.Type);

        public static IEnumerable<(Type type, string[] scope)> GetRequiredConfigurations(Type applicationType)
            => GetAttributes<RequiresConfiguration>(applicationType).Select(req => (req.Type, req.Scope));

        public static IEnumerable<(Type type, string[] scope)> GetRequiredSecretConfigurations(Type applicationType)
            => GetAttributes<RequiresSecretConfiguration>(applicationType).Select(req => (req.Type, req.Scope));

        private static IEnumerable<TAttribute> GetAttributes<TAttribute>(Type applicationType)
            where TAttribute : Attribute => applicationType.GetCustomAttributes<TAttribute>(true);
    }
}