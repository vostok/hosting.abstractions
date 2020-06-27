using System.Collections.Generic;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Composite
{
    [PublicAPI]
    public static class ICompositeApplicationBuilderExtensions
    {
        [NotNull]
        public static ICompositeApplicationBuilder UseParallelInitialization([NotNull] this ICompositeApplicationBuilder builder)
            => builder.CustomizeSettings(settings => settings.UseParallelInitialization = true);

        [NotNull]
        public static ICompositeApplicationBuilder UseSequentialInitialization([NotNull] this ICompositeApplicationBuilder builder)
            => builder.CustomizeSettings(settings => settings.UseParallelInitialization = false);

        [NotNull]
        public static ICompositeApplicationBuilder UseParallelExecution([NotNull] this ICompositeApplicationBuilder builder)
            => builder.CustomizeSettings(settings => settings.UseParallelExecution = true);

        [NotNull]
        public static ICompositeApplicationBuilder UseSequentialExecution([NotNull] this ICompositeApplicationBuilder builder)
            => builder.CustomizeSettings(settings => settings.UseParallelExecution = false);

        /// <inheritdoc cref="ICompositeApplicationBuilder.AddApplication"/>
        [NotNull]
        public static ICompositeApplicationBuilder AddApplications(
            [NotNull] this ICompositeApplicationBuilder builder,
            [NotNull] IEnumerable<IVostokApplication> applications)
        {
            foreach (var application in applications)
                builder.AddApplication(application);

            return builder;
        }
    }
}
