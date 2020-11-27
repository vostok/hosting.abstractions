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

        /// <inheritdoc cref="Check(IVostokApplication,IVostokHostingEnvironment)"/>
        [Obsolete("This method was deprecated with addition of CompositeApplication. Please use the overload with IVostokApplication instead.")]
        public static void Check(
            [NotNull] Type applicationType,
            [NotNull] IVostokHostingEnvironment environment)
        {
            if (!TryCheck(applicationType, environment, out var errors))
                throw new RequirementsCheckException(applicationType, errors);
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
            CheckHostExtensions(application, environment, errors);
            CheckConfigurationTypes(application, environment, errors);

            return errors.Count == 0;
        }

        /// <inheritdoc cref="TryCheck(IVostokApplication,IVostokHostingEnvironment,out List{string})"/>
        [Obsolete("This method was deprecated with addition of CompositeApplication. Please use the overload with IVostokApplication instead.")]
        public static bool TryCheck(
            [NotNull] Type applicationType,
            [NotNull] IVostokHostingEnvironment environment,
            [NotNull] out List<string> errors)
        {
            if (applicationType == null)
                throw new ArgumentNullException(nameof(applicationType));

            if (environment == null)
                throw new ArgumentNullException(nameof(environment));

            errors = new List<string>();

            CheckPort(applicationType, environment, errors);
            CheckHostExtensions(applicationType, environment, errors);
            CheckConfigurationTypes(applicationType, environment, errors);

            return errors.Count == 0;
        }

        #region CheckPort
        private static void CheckPort(Type applicationType, IVostokHostingEnvironment environment, List<string> errors)
            => CheckPortInternal(() => RequirementDetector.RequiresPort(applicationType), environment, errors);

        private static void CheckPort(IVostokApplication application, IVostokHostingEnvironment environment, List<string> errors)
            => CheckPortInternal(() => RequirementDetector.RequiresPort(application), environment, errors);

        private static void CheckPortInternal(
            Func<bool> requiresPort,
            IVostokHostingEnvironment environment,
            List<string> errors)
        {
            if (requiresPort() && environment.Port == null)
                errors.Add("Application requires a port, which is not provided by host (see 'SetPort' extension when configuring hosting environment).");
        } 
        #endregion

        #region CheckHostExtensions

        private static void CheckHostExtensions(Type applicationType, IVostokHostingEnvironment environment, List<string> errors)
            => CheckHostExtensionsInternal(() => RequirementDetector.GetRequiredHostExtensions(applicationType), environment, errors);

        private static void CheckHostExtensions(IVostokApplication application, IVostokHostingEnvironment environment, List<string> errors)
            => CheckHostExtensionsInternal(() => RequirementDetector.GetRequiredHostExtensions(application), environment, errors);

        private static void CheckHostExtensionsInternal(
            Func<IEnumerable<RequiresHostExtension>> getRequiredHostExtensions,
            IVostokHostingEnvironment environment,
            List<string> errors)
        {
            var registeredExtensions = new HashSet<Type>(environment.HostExtensions.GetAll().Select(ext => ext.Item1));

            foreach (var requiredExtension in getRequiredHostExtensions().Where(h => h.Key == null))
            {
                if (!registeredExtensions.Contains(requiredExtension.Type))
                    errors.Add($"Application requires a host extension of type '{requiredExtension.Type}', which is not registered by host (see IVostokHostingEnvironmentBuilder.SetupHostExtensions).");
            }

            foreach (var requiredExtension in getRequiredHostExtensions().Where(h => h.Key != null))
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

        #endregion

        #region CheckConfigurationTypes

        private static void CheckConfigurationTypes(Type applicationType, IVostokHostingEnvironment environment, List<string> errors)
           => CheckConfigurationTypesInternal(
               () => RequirementDetector.GetRequiredConfigurations(applicationType),
               () => RequirementDetector.GetRequiredSecretConfigurations(applicationType),
               Enumerable.Empty<RequiresMergedConfiguration>,
               environment,
               errors);

        private static void CheckConfigurationTypes(IVostokApplication application, IVostokHostingEnvironment environment, List<string> errors)
            => CheckConfigurationTypesInternal(
                () => RequirementDetector.GetRequiredConfigurations(application),
                () => RequirementDetector.GetRequiredSecretConfigurations(application),
                () => RequirementDetector.GetRequiredMergedConfigurations(application),
                environment,
                errors);

        private static void CheckConfigurationTypesInternal(
            Func<IEnumerable<RequiresConfiguration>> getRequiredConfigurations,
            Func<IEnumerable<RequiresSecretConfiguration>> getRequiredSecretConfigurations,
            Func<IEnumerable<RequiresMergedConfiguration>> getRequiredMergedConfigurations,
            IVostokHostingEnvironment environment,
            List<string> errors)
        {
            var requiredConfigurations = getRequiredConfigurations()
                .Select(requirement => requirement.Type);

            var requiredSecretConfigurations = getRequiredSecretConfigurations()
                .Select(requirement => requirement.Type);

            var requiredMergedConfigurations = getRequiredMergedConfigurations()
                .Select(requirement => requirement.Type);

            foreach (var type in requiredConfigurations)
                CheckConfigurationType(environment.ConfigurationProvider, type, errors);

            foreach (var type in requiredSecretConfigurations)
                CheckConfigurationType(environment.SecretConfigurationProvider, type, errors);

            foreach (var type in requiredMergedConfigurations)
                CheckConfigurationType(environment.ConfigurationProvider, type, errors);
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

        #endregion
    }
}