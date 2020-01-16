using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions
{
    [PublicAPI]
    public static class WellKnownApplicationIdentityProperties
    {
        /// <summary>
        /// Property that denotes <see cref="IVostokApplicationIdentity.Project"/>.
        /// </summary>
        public const string Project = "project";

        /// <summary>
        /// Property that denotes <see cref="IVostokApplicationIdentity.Subproject"/>.
        /// </summary>
        public const string Subproject = "subproject";

        /// <summary>
        /// Property that denotes <see cref="IVostokApplicationIdentity.Environment"/>.
        /// </summary>
        public const string Environment = "environment";

        /// <summary>
        /// Property that denotes <see cref="IVostokApplicationIdentity.Application"/>.
        /// </summary>
        public const string Application = "application";

        /// <summary>
        /// Property that denotes <see cref="IVostokApplicationIdentity.Instance"/>.
        /// </summary>
        public const string Instance = "instance";
    }
}