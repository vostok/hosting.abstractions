using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using JetBrains.Annotations;
using NUnit.Framework;
using Vostok.Hosting.Abstractions.Requirements;

namespace Vostok.Hosting.Abstractions.Tests.Requirements
{
    [TestFixture]
    internal class RequirementDetector_Tests
    {
        [Test]
        public void Should_detect_attributes_derived_from_RequiresHostingExtension()
        {
            RequirementDetector
                .GetRequiredHostExtensions(typeof(Application))
                .Should()
                .ContainSingle()
                .Which.Type.Should()
                .Be<IGenericInterface<string>>();
        }

        [Test]
        public void Should_detect_attributes_from_base_classes()
        {
            RequirementDetector.RequiresPort(typeof(Application)).Should().BeTrue();
        }

        [Test]
        public void Should_detect_attributes_from_static_property()
        {
            var applicationType = new GenericApplication<int, string>().GetType();

            var extensions = RequirementDetector.GetRequiredHostExtensions(applicationType).ToArray();

            extensions.Should().HaveCount(2);

            extensions[0].Type.Should().Be(typeof(int));
            extensions[1].Type.Should().Be(typeof(string));

            RequirementDetector.RequiresPort(applicationType).Should().BeTrue();
        }

        [Test]
        public void Should_detect_attributes_from_static_property_of_base_class()
        {
            var applicationType = new DerivedGenericApplication<string>().GetType();

            var extensions = RequirementDetector.GetRequiredHostExtensions(applicationType).ToArray();

            extensions.Should().HaveCount(2);

            extensions[0].Type.Should().Be(typeof(Guid));
            extensions[1].Type.Should().Be(typeof(string));

            RequirementDetector.RequiresPort(applicationType).Should().BeTrue();
        }

        [RequiresPort]
        private class ApplicationBase
        {

        }

        [RequiresClosedInterface(typeof(string))]
        private class Application : ApplicationBase
        {
        }

        private class GenericApplication<T1, T2>
        {
            [UsedImplicitly]
            public static IEnumerable<Attribute> AdditionalRequirements
            {
                get
                {
                    yield return new RequiresHostExtension(typeof(T1));
                    yield return new RequiresHostExtension(typeof(T2));
                    yield return new RequiresPort();
                }
            }

        }

        private class DerivedGenericApplication<T> : GenericApplication<Guid, T>
        {
        }

        private class RequiresClosedInterface : RequiresHostExtension
        {
            public RequiresClosedInterface([NotNull] Type type)
                : base(typeof(IGenericInterface<>).MakeGenericType(type))
            {
            }
        }

        // ReSharper disable once UnusedTypeParameter
        private interface IGenericInterface<T>
        {
        }
    }
}