using System;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Diagnostics
{
    [PublicAPI]
    public static class IDiagnosticInfoProviderExtensions
    {
        public static object QuerySafe([NotNull] this IDiagnosticInfoProvider provider)
        {
            try
            {
                return provider.Query();
            }
            catch (Exception error)
            {
                return $"ERROR: info provider failed with {error.GetType().Name}: '{error.Message}'";
            }
        }
    }
}
