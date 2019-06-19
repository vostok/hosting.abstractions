using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions
{
    [PublicAPI]
    public interface IVostokApplicationIdentity
    {
        /// <summary>
        /// <para>Name of the project the application belongs to.</para>
        /// <para>In real-life terms, it may translate into teams, products or other types of groups.</para>
        /// </summary>
        [NotNull]
        string Project { get; }

        /// <summary>
        /// <para>Name of the environment the application is currently deployed in.</para>
        /// <para>Every environment belongs to a single project, but a project may contain multiple environments.</para>
        /// <para><c>dev</c>, <c>staging</c> and <c>prod</c> are notable examples of environments used to implement release lifecycle.</para>
        /// <para>Unique within corresponding <see cref="Project"/>.</para>
        /// </summary>
        [NotNull]
        string Environment { get; }

        /// <summary>
        /// <para>Name of the application itself.</para>
        /// <para>Unique within corresponding <see cref="Project"/>.</para>
        /// </summary>
        [NotNull]
        string Application { get; }

        /// <summary>
        /// <para>Name of the current application instance (in case there are multiple).</para>
        /// <para>Unique within corresponding <see cref="Environment"/> and <see cref="Application"/>.</para>
        /// <para>Implementation is host-specific: it may return replica numbers, DNS names or identifiers of arbitrary nature.</para>
        /// </summary>
        [NotNull]
        string Instance { get; }
    }
}