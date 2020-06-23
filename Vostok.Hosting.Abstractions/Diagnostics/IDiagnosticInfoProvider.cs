using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Diagnostics
{
    [PublicAPI]
    public interface IDiagnosticInfoProvider
    {
        /// <summary>
        /// Returns an arbitrary object that describes internal state of an application component.
        /// </summary>
        object Invoke();
    }
}
