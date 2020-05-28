using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using JetBrains.Annotations;
using NUnit.Framework;
using Vostok.Hosting.Abstractions.Composite;
using Vostok.Hosting.Abstractions.Requirements;

namespace Vostok.Hosting.Abstractions.Tests
{
    [TestFixture]
    internal class RequirementDetector_Tests
    {
        [Test]
        public void Should_detect_attributes_derived_from_RequiresHostingExtension()
        {
            RequirementDetector
                .GetRequiredHostExtensions(new Application())
                .Should()
                .ContainSingle()
                .Which.Type.Should()
                .Be<IGenericInterface<string>>();
        }

        [Test]
        public void Should_detect_attributes_from_base_classes()
        {
            RequirementDetector.RequiresPort(new Application()).Should().BeTrue();
        }

        [Test]
        public void Should_detect_attributes_from_static_property()
        {
            var application = new GenericApplication<int, string>();

            var extensions = RequirementDetector.GetRequiredHostExtensions(application).ToArray();

            extensions.Should().HaveCount(2);

            extensions[0].Type.Should().Be(typeof(int));
            extensions[1].Type.Should().Be(typeof(string));

            RequirementDetector.RequiresPort(application).Should().BeTrue();
        }

        [Test]
        public void Should_detect_attributes_from_static_property_of_base_class()
        {
            var application = new DerivedGenericApplication<string>();

            var extensions = RequirementDetector.GetRequiredHostExtensions(application).ToArray();

            extensions.Should().HaveCount(2);

            extensions[0].Type.Should().Be(typeof(Guid));
            extensions[1].Type.Should().Be(typeof(string));

            RequirementDetector.RequiresPort(application).Should().BeTrue();
        }

        [Test]
        public void Should_support_composite_applications()
        {
            var application = new MultiApplication();

            RequirementDetector.RequiresPort(application).Should().BeTrue();

            RequirementDetector.GetRequiredConfigurations(application)
                .Should()
                .ContainSingle()
                .Which.Type.Should()
                .Be(typeof(string));

            var extensions = RequirementDetector.GetRequiredHostExtensions(application).ToArray();

            extensions.Should().HaveCount(4);

            extensions[0].Type.Should().Be(typeof(IGenericInterface<string>));
            extensions[1].Type.Should().Be(typeof(IGenericInterface<string>));
            extensions[2].Type.Should().Be(typeof(Guid));
            extensions[3].Type.Should().Be(typeof(int));
        }

        [RequiresPort]
        private class ApplicationBase : EmptyApplication
        {
        }

        [RequiresClosedInterface(typeof(string))]
        private class Application : ApplicationBase
        {
        }

        private class GenericApplication<T1, T2> : EmptyApplication
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

        [RequiresConfiguration(typeof(string))]
        private class AppWithConfiguration : EmptyApplication
        {
        }

        private class MultiApplication : CompositeApplication
        {
            public MultiApplication()
                : base(Build)
            {
            }

            private static void Build(ICompositeApplicationBuilder builder)
            {
                builder.AddApplication(new AppWithConfiguration());
                builder.AddApplication(new Application());
                builder.AddApplication(new Application());
                builder.AddApplication(new DerivedGenericApplication<int>());
            }
        }
    }
}