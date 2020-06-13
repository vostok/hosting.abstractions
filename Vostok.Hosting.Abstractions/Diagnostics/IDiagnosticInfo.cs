using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Diagnostics
{
    /// <inheritdoc cref="IVostokApplicationDiagnostics.Info"/>
    [PublicAPI]
    public interface IDiagnosticInfo
    {
        /// <summary>
        /// <para>Registers given info <paramref name="provider"/> under given <paramref name="entry"/> so that it can be queried later.</para>
        /// <para>Fails with an exception if the entry is already occupied.</para>
        /// <para>Dispose of the returned result to cancel the registration and remove the provider from use.</para>
        /// </summary>
        [NotNull]
        IDisposable RegisterProvider([NotNull] DiagnosticEntry entry, [NotNull] IDiagnosticInfoProvider provider);

        /// <summary>
        /// Returns the entries of all registered info providers.
        /// </summary>
        [NotNull]
        IReadOnlyList<DiagnosticEntry> ListAll();

        /// <summary>
        /// Queries the info provider registered under given <paramref name="entry"/>.
        /// </summary>
        bool TryQuery([NotNull] DiagnosticEntry entry, out object info);
    }
}
