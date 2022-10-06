using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions
{
    /// <inheritdoc cref="IVostokHostingEnvironment.HostExtensions"/>
    [PublicAPI]
    public interface IVostokHostKeyedExtensions : IVostokHostExtensions
    {
        /// <summary>
        /// Returns all types registered with key.
        /// </summary>
        IEnumerable<(string, Type, object)> GetAllKeyed();
    }
}