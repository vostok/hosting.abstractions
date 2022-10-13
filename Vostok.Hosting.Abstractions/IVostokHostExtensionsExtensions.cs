using System;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions
{
    [PublicAPI]
    public static class IVostokHostExtensionsExtensions
    {
        /// <summary>
        /// <para>Attempts to convert given <paramref name="extensions"/> to <see cref="IVostokHostMutableExtensions"/> for modification.</para>
        /// <para>Fails with <see cref="InvalidOperationException"/> if that's not possible.</para>
        /// </summary>
        [NotNull]
        public static IVostokHostMutableExtensions AsMutable([NotNull] this IVostokHostExtensions extensions)
            => extensions as IVostokHostMutableExtensions ?? throw new InvalidOperationException($"Host extensions of type '{extensions.GetType().Name}' are not mutable.");
        
        /// <summary>
        /// <para>Attempts to convert given <paramref name="extensions"/> to <see cref="IVostokHostKeyedExtensions"/> for get all keyed.</para>
        /// <para>Fails with <see cref="InvalidOperationException"/> if that's not possible.</para>
        /// </summary>
        [NotNull]
        public static IVostokHostKeyedExtensions AsKeyed([NotNull] this IVostokHostExtensions extensions)
            => extensions as IVostokHostKeyedExtensions ?? throw new InvalidOperationException($"Host extensions of type '{extensions.GetType().Name}' are not {nameof(IVostokHostKeyedExtensions)}.");
    }
}
