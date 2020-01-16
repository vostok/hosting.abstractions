using System;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Requirements
{
    /// <summary>
    /// <para>Denotes that application requires the host to register an extension of type <see cref="Type"/> in <see cref="IVostokHostingEnvironment.HostExtensions"/>.</para>
    /// <para>If <see cref="Key"/> is <c>null</c>, use <see cref="IVostokHostExtensions.Get{TExtension}()"/> method to obtain host extension.</para>
    /// <para>If <see cref="Key"/> is not <c>null</c>, use <see cref="IVostokHostExtensions.Get{TExtension}(string)"/> method to obtain host extension.</para>
    /// </summary>
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
    public class RequiresHostExtension : Attribute
    {
        [NotNull]
        public readonly Type Type;

        [CanBeNull]
        public readonly string Key;

        public RequiresHostExtension([NotNull] Type type, [CanBeNull] string key = null)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Key = key;
        }
    }
}