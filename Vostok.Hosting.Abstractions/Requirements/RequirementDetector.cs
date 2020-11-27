using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Requirements
{
    /// <summary>
    /// Provides helper methods to detect application requirements, such as <see cref="RequiresConfiguration"/> or <see cref="RequiresHostExtension"/>.
    /// </summary>
    [PublicAPI]
    public static class RequirementDetector
    {
        public static bool RequiresPort([NotNull] IVostokApplication application)
            => RequirementAttributesHelper.GetAttributes<RequiresPort>(application).Any();

        [ItemNotNull]
        public static IEnumerable<RequiresHostExtension> GetRequiredHostExtensions([NotNull] IVostokApplication application)
            => RequirementAttributesHelper.GetAttributes<RequiresHostExtension>(application);

        [ItemNotNull]
        public static IEnumerable<RequiresConfiguration> GetRequiredConfigurations([NotNull] IVostokApplication application)
            => RequirementAttributesHelper.GetAttributes<RequiresConfiguration>(application);

        [ItemNotNull]
        public static IEnumerable<RequiresSecretConfiguration> GetRequiredSecretConfigurations([NotNull] IVostokApplication application)
            => RequirementAttributesHelper.GetAttributes<RequiresSecretConfiguration>(application);

        [ItemNotNull]
        public static IEnumerable<RequiresMergedConfiguration> GetRequiredMergedConfigurations([NotNull] IVostokApplication application)
            => RequirementAttributesHelper.GetAttributes<RequiresMergedConfiguration>(application);

        [Obsolete("This method was deprecated with addition of CompositeApplication. Please use the overload with IVostokApplication instead.")]
        public static bool RequiresPort([NotNull] Type applicationType)
            => RequirementAttributesHelper.GetAttributes<RequiresPort>(applicationType).Any();

        [ItemNotNull, Obsolete("This method was deprecated with addition of CompositeApplication. Please use the overload with IVostokApplication instead.")]
        public static IEnumerable<RequiresHostExtension> GetRequiredHostExtensions([NotNull] Type applicationType)
            => RequirementAttributesHelper.GetAttributes<RequiresHostExtension>(applicationType);

        [ItemNotNull, Obsolete("This method was deprecated with addition of CompositeApplication. Please use the overload with IVostokApplication instead.")]
        public static IEnumerable<RequiresConfiguration> GetRequiredConfigurations([NotNull] Type applicationType)
            => RequirementAttributesHelper.GetAttributes<RequiresConfiguration>(applicationType);

        [ItemNotNull, Obsolete("This method was deprecated with addition of CompositeApplication. Please use the overload with IVostokApplication instead.")]
        public static IEnumerable<RequiresSecretConfiguration> GetRequiredSecretConfigurations([NotNull] Type applicationType)
            => RequirementAttributesHelper.GetAttributes<RequiresSecretConfiguration>(applicationType);
    }
}