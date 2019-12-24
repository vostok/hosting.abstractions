using System;
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

        [RequiresClosedInterface(typeof(string))]
        private class Application
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
