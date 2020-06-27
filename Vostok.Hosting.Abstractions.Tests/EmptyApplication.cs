using System.Threading.Tasks;

namespace Vostok.Hosting.Abstractions.Tests
{
    internal class EmptyApplication : IVostokApplication
    {
        public Task InitializeAsync(IVostokHostingEnvironment environment) =>
            Task.CompletedTask;

        public Task RunAsync(IVostokHostingEnvironment environment) =>
            Task.CompletedTask;
    }
}
