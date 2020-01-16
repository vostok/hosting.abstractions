using System;
using System.Reflection;

namespace Vostok.Hosting.Abstractions.Helpers
{
    internal static class IVostokHostExtensionsExtensions
    {
        private static readonly MethodInfo GetMethod = typeof(IVostokHostExtensions)
            .GetMethod(nameof(IVostokHostExtensions.Get), new[] {typeof(string)});

        public static object Get(this IVostokHostExtensions extensions, Type type, string key)
            => GetMethod.MakeGenericMethod(type).Invoke(extensions, new object[] {key});
    }
}