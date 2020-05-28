using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vostok.Hosting.Abstractions.Composite;

namespace Vostok.Hosting.Abstractions.Requirements
{
    internal static class RequirementAttributesHelper
    {
        public static IEnumerable<TRequirement> GetAttributes<TRequirement>(IVostokApplication application)
            where TRequirement : Requirement
            => GetApplicationTypes(application).SelectMany(GetAttributes<TRequirement>);

        public static IEnumerable<TRequirement> GetAttributes<TRequirement>(Type applicationType)
            where TRequirement : Requirement
            => GetDirectAttributes<TRequirement>(applicationType).Concat(GetStaticAttributes<TRequirement>(applicationType));

        private static IEnumerable<TRequirement> GetDirectAttributes<TRequirement>(Type applicationType)
            where TRequirement : Requirement
            => applicationType?.GetCustomAttributes<TRequirement>(true) ?? Enumerable.Empty<TRequirement>();

        private static IEnumerable<TRequirement> GetStaticAttributes<TRequirement>(Type applicationType)
            where TRequirement : Requirement
        {
            var property = applicationType.GetProperty("AdditionalRequirements", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy,
                null, typeof(IEnumerable<Attribute>), Array.Empty<Type>(), null);

            return property == null
                ? Enumerable.Empty<TRequirement>()
                : ((IEnumerable<Attribute>)property.GetValue(null)).Where(attribute => attribute is TRequirement).Cast<TRequirement>();
        }

        private static IEnumerable<Type> GetApplicationTypes(IVostokApplication application)
        {
            yield return application.GetType();

            if (application is CompositeApplication compositeApplication)
            {
                foreach (var type in compositeApplication.ApplicationTypes)
                    yield return type;
            }
        }
    }
}
