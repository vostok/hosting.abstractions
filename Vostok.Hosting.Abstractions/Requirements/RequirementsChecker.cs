using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Vostok.Configuration.Abstractions;
using Vostok.Hosting.Abstractions.Helpers;

namespace Vostok.Hosting.Abstractions.Requirements
{
    /// <summary>
    /// A helper that allows to check whether given instance of <see cref="IVostokHostingEnvironment"/> satisfies all of the application's requirements expressed via attributes.
    /// </summary>
    [PublicAPI]
    public static class RequirementsChecker
    {
        /// <summary>
        /// Checks whether given <paramref name="environment"/> satisfies all requirements imposed by <paramref name="application"/>.
        /// </summary>
        /// <exception cref="RequirementsCheckException">Some of application's requirements were not satisfied.</exception>
        public static void Check(
            [NotNull] IVostokApplication application,
            [NotNull] IVostokHostingEnvironment environment)
        {
            if (!TryCheck(application, environment, out var errors))
                throw new RequirementsCheckException(application.GetType(), errors);
        }

        /// <summary>
        /// Checks whether given <paramref name="environment"/> satisfies all requirements imposed by <paramref name="application"/>.
        /// </summary>
        /// <returns><c>true</c> if all requirements are met, <c>false</c> otherwise (with at least one element in <paramref name="errors"/>).</returns>
        public static bool TryCheck(
            [NotNull] IVostokApplication application,
            [NotNull] IVostokHostingEnvironment environment,
            [NotNull] out List<string> errors)
        {
            if (application == null)
                throw new ArgumentNullException(nameof(application));

            if (environment == null)
                throw new ArgumentNullException(nameof(environment));

            errors = new List<string>();

            CheckPort(application, environment, errors);
            CheckHostingExtensions(application, environment, errors);
            CheckConfigurationTypes(application, environment, errors);

            return errors.Count == 0;
        }

        private static void CheckPort(IVostokApplication application, IVostokHostingEnvironment environment, List<string> errors)
        {
            if (RequirementDetector.RequiresPort(application) && environment.Port == null)
                errors.Add("Application requires a port, which is not provided by host (see 'SetPort' extension when configuring hosting environment).");
        }

        private static void CheckHostingExtensions(IVostokApplication application, IVostokHostingEnvironment environment, List<string> errors)
        {
            var registeredExtensions = new HashSet<Type>(environment.HostExtensions.GetAll().Select(ext => ext.Item1));

            foreach (var requiredExtension in RequirementDetector.GetRequiredHostExtensions(application).Where(h => h.Key == null))
            {
                if (!registeredExtensions.Contains(requiredExtension.Type))
                    errors.Add($"Application requires a host extension of type '{requiredExtension.Type}', which is not registered by host (see IVostokHostingEnvironmentBuilder.SetupHostExtensions).");
            }

            foreach (var requiredExtension in RequirementDetector.GetRequiredHostExtensions(application).Where(h => h.Key != null))
            {
                try
                {
                    environment.HostExtensions.Get(requiredExtension.Type, requiredExtension.Key);
                }
                catch
                {
                    errors.Add($"Application requires a host extension of type '{requiredExtension.Type}' with key '{requiredExtension.Key}', which is not registered by host (see IVostokHostingEnvironmentBuilder.SetupHostExtensions).");
                }
            }
        }

        private static void CheckConfigurationTypes(IVostokApplication application, IVostokHostingEnvironment environment, List<string> errors)
        {
            var requiredConfigurations = RequirementDetector
                .GetRequiredConfigurations(application)
                .Select(requirement => requirement.Type);

            var requiredSecretConfigurations = RequirementDetector
                .GetRequiredSecretConfigurations(application)
                .Select(requirement => requirement.Type);

            foreach (var type in requiredConfigurations)
                CheckConfigurationType(environment.ConfigurationProvider, type, errors);

            foreach (var type in requiredSecretConfigurations)
                CheckConfigurationType(environment.SecretConfigurationProvider, type, errors);
        }

        private static void CheckConfigurationType(IConfigurationProvider provider, Type type, List<string> errors)
        {
            try
            {
                provider.Get(type);
            }
            catch (Exception error)
            {
                errors.Add(error.InnerException?.Message ?? error.Message);
            }
        }
    }
}