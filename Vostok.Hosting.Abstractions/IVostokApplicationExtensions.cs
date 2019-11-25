using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using Vostok.Hosting.Abstractions.Requirements;

namespace Vostok.Hosting.Abstractions
{
    [PublicAPI]
    public static class IVostokApplicationExtensions
    {
        /// <inheritdoc cref="IVostokApplication.InitializeAsync"/>
        public static void Initialize(
            [NotNull] this IVostokApplication application,
            [NotNull] IVostokHostingEnvironment environment)
            => application.InitializeAsync(environment).GetAwaiter().GetResult();

        /// <inheritdoc cref="IVostokApplication.RunAsync"/>
        public static void Run(
            [NotNull] this IVostokApplication application,
            [NotNull] IVostokHostingEnvironment environment)
            => application.RunAsync(environment).GetAwaiter().GetResult();

        /// <summary>
        /// Initializes given <paramref name="application"/> and then runs it.
        /// </summary>
        public static void InitializeAndRun(
            [NotNull] this IVostokApplication application,
            [NotNull] IVostokHostingEnvironment environment)
        {
            application.Initialize(environment);
            application.Run(environment);
        }

        /// <summary>
        /// Returns whether is port required for the given <paramref name="vostokApplication"/>.
        /// </summary>
        public static bool IsHostPortRequired([NotNull] this IVostokApplication vostokApplication)
        {
            return vostokApplication.GetType().GetCustomAttribute<RequiresPortAttribute>(true) != null;
        }

        /// <summary>
        /// Returns all types, that should be setted up into <see cref="IVostokHostingEnvironment.ConfigurationProvider"/> for the given <paramref name="vostokApplication"/>.
        /// </summary>
        public static IReadOnlyList<Type> GetRequiredHostConfigurationProviderTypes([NotNull] this IVostokApplication vostokApplication)
        {
            var result = new List<Type>();

            foreach (var attribute in vostokApplication.GetType().GetCustomAttributes(true))
            {
                if (attribute is RequiresConfiguration typeAttribute)
                    result.Add(typeAttribute.Type);
            }

            return result;
        }

        /// <summary>
        /// Returns all types, that should be provided in <see cref="IVostokHostingEnvironment.HostExtensions"/> for the given <paramref name="vostokApplication"/>.
        /// </summary>
        public static IReadOnlyList<Type> GetRequiredHostExtensionsTypes([NotNull] this IVostokApplication vostokApplication)
        {
            var result = new List<Type>();

            foreach (var attribute in vostokApplication.GetType().GetCustomAttributes(true))
            {
                if (attribute is RequiresHostExtensionAttribute extensionRequired)
                    result.Add(extensionRequired.Type);
            }

            return result;
        }
    }
}
