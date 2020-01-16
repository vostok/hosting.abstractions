using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions
{
    /// <inheritdoc cref="IVostokHostingEnvironment.HostExtensions"/>
    [PublicAPI]
    public interface IVostokHostExtensions
    {
        /// <summary>
        /// <para>Returns a host-provided extension of type <typeparamref name="TExtension"/>.</para>
        /// <para>Throws an exception if no extension of given type is registered.</para>
        /// </summary>
        TExtension Get<TExtension>();

        /// <summary>
        /// <para>Returns a host-provided extension of type <typeparamref name="TExtension"/> corresponding to given <paramref name="key"/>.</para>
        /// <para>Throws an exception if no extension of given type is registered with given key.</para>
        /// </summary>
        TExtension Get<TExtension>([NotNull] string key);

        /// <summary>
        /// <para>Attempts to return a host-provided extension of type <typeparamref name="TExtension"/>.</para>
        /// <para>Returns <c>false</c> if no extension of given type is registered.</para>
        /// </summary>
        bool TryGet<TExtension>(out TExtension result);

        /// <summary>
        /// <para>Attempts to return a host-provided extension of type <typeparamref name="TExtension"/> corresponding to given <paramref name="key"/>.</para>
        /// <para>Returns <c>false</c> if no extension of given type is registered with given key.</para>
        /// </summary>
        bool TryGet<TExtension>([NotNull] string key, out TExtension result);

        /// <summary>
        /// Returns all types, registered without key.
        /// </summary>
        IEnumerable<(Type, object)> GetAll();
    }
}