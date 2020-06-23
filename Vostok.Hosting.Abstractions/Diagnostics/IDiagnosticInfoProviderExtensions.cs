using System;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Diagnostics
{
    [PublicAPI]
    public static class IDiagnosticInfoProviderExtensions
    {
        public static object InvokeSafe([NotNull] this IDiagnosticInfoProvider provider)
        {
            try
            {
                return provider.Invoke();
            }
            catch (Exception error)
            {
                return $"ERROR: diagnostic info provider failed with {error.GetType().Name}: '{error.Message}'";
            }
        }
    }
}
