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
        /// <para>Attempts to convert given <paramref name="extensions"/> to <see cref="IVostokHostExtensionsForKeyed"/> for get all keyed.</para>
        /// <para>Fails with <see cref="InvalidOperationException"/> if that's not possible.</para>
        /// </summary>
        [NotNull]
        public static IVostokHostExtensionsForKeyed AsForKeyed([NotNull] this IVostokHostExtensions extensions)
            => extensions as IVostokHostExtensionsForKeyed ?? throw new InvalidOperationException($"Host extensions of type '{extensions.GetType().Name}' are not {nameof(IVostokHostExtensionsForKeyed)}.");

        /// <summary>
        /// <para>Attempts to convert given <paramref name="extensions"/> to <see cref="IVostokHostExtensionsForKeyed"/> for get all keyed.</para>
        /// <para>Fails with <see cref="InvalidOperationException"/> if that's not possible.</para>
        /// </summary>
        [NotNull]
        public static bool TryAsForKeyed([NotNull] this IVostokHostExtensions extensions, out IVostokHostExtensionsForKeyed forKeyed)
        {
            forKeyed = extensions as IVostokHostExtensionsForKeyed;

            return forKeyed != null;
        }
    }
}
