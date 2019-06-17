using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions
{
    [PublicAPI]
    public static class IVostokApplicationExtensions
    {
        public static void Initialize(
            [NotNull] this IVostokApplication application,
            [NotNull] IVostokHostingEnvironment environment)
            => application.InitializeAsync(environment).GetAwaiter().GetResult();

        public static void Run(
            [NotNull] this IVostokApplication application,
            [NotNull] IVostokHostingEnvironment environment)
            => application.RunAsync(environment).GetAwaiter().GetResult();

        public static void InitializeAndRun(
            [NotNull] this IVostokApplication application,
            [NotNull] IVostokHostingEnvironment environment)
        {
            application.Initialize(environment);
            application.Run(environment);
        }
    }
}
