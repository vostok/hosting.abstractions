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

        public static bool RequiresPort([NotNull] IVostokApplication application, out int count)
            => (count = RequirementAttributesHelper.GetAttributes<RequiresPort>(application).Count()) > 0;

        [ItemNotNull]
        public static IEnumerable<RequiresHostExtension> GetRequiredHostExtensions([NotNull] IVostokApplication application)
            => RequirementAttributesHelper.GetAttributes<RequiresHostExtension>(application);

        [ItemNotNull]
        public static IEnumerable<RequiresConfiguration> GetRequiredConfigurations([NotNull] IVostokApplication application)
            => RequirementAttributesHelper.GetAttributes<RequiresConfiguration>(application);

        [ItemNotNull]
        public static IEnumerable<RequiresSecretConfiguration> GetRequiredSecretConfigurations([NotNull] IVostokApplication application)
            => RequirementAttributesHelper.GetAttributes<RequiresSecretConfiguration>(application);
    }
}