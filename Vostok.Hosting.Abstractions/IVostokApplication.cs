using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions
{
    // TODO(iloktionov): xml-docs

    [PublicAPI]
    public interface IVostokApplication
    {
        [NotNull]
        Task InitializeAsync([NotNull] IVostokHostingEnvironment environment);

        [NotNull]
        Task RunAsync([NotNull] IVostokHostingEnvironment environment);
    }
}