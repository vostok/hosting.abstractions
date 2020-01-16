using System;
using System.Reflection;
using Vostok.Configuration.Abstractions;

namespace Vostok.Hosting.Abstractions.Helpers
{
    internal static class IConfigurationProviderExtensions
    {
        private static readonly MethodInfo GetMethod = typeof(IConfigurationProvider)
            .GetMethod(nameof(IConfigurationProvider.Get), new Type[] {});

        public static object Get(this IConfigurationProvider provider, Type type)
            => GetMethod.MakeGenericMethod(type).Invoke(provider, Array.Empty<object>());
    }
}