using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using Vostok.Configuration.Abstractions;
using Vostok.Hosting.Abstractions.Requirements;

namespace Vostok.Hosting.Abstractions.Tests
{
    [TestFixture]
    internal class RequirementChecker_Tests
    {
        private IVostokApplication application;
        private IVostokHostingEnvironment environment;
        private TestHostExtensions hostExtensions;
        private IConfigurationProvider configProvider;
        private IConfigurationProvider secretConfigProvider;

        [SetUp]
        public void TestSetup()
        {
            application = new Application();

            hostExtensions = new TestHostExtensions();

            environment = Substitute.For<IVostokHostingEnvironment>();
            environment.HostExtensions.Returns(_ => hostExtensions);
            environment.ConfigurationProvider.Returns(configProvider = Substitute.For<IConfigurationProvider>());
            environment.SecretConfigurationProvider.Returns(secretConfigProvider = Substitute.For<IConfigurationProvider>());
            environment.Port.Returns(123);

            hostExtensions.Add(Guid.Empty);
            hostExtensions.Add("key", Guid.Empty);

            configProvider.Get<string>().Returns(string.Empty);
            secretConfigProvider.Get<string>().Returns(string.Empty);
        }

        [Test]
        public void Should_succeed_if_all_requirements_are_met()
            => RequirementsChecker.Check(application, environment);

        [Test]
        public void Should_fail_if_port_is_not_provided()
        {
            environment.Port.Returns(null as int?);

            ShouldFail();
        }

        [Test]
        public void Should_fail_if_config_is_not_provided()
        {
            configProvider.Get<string>().Throws(new Exception("Config not found"));

            ShouldFail();
        }

        [Test]
        public void Should_fail_if_secret_config_is_not_provided()
        {
            secretConfigProvider.Get<string>().Throws(new Exception("Config not found"));

            ShouldFail();
        }

        [Test]
        public void Should_fail_if_host_extensions_are_not_provided()
        {
            hostExtensions = new TestHostExtensions();

            ShouldFail();
        }

        private void ShouldFail()
        {
            RequirementsChecker.TryCheck(application, environment, out var errors).Should().BeFalse();

            errors.Should().NotBeEmpty();

            Console.Out.WriteLine(string.Join(Environment.NewLine, errors));
        }

        [RequiresPort]
        [RequiresConfiguration(typeof(string))]
        [RequiresSecretConfiguration(typeof(string))]
        [RequiresMergedConfiguration(typeof(int))]
        [RequiresHostExtension(typeof(Guid))]
        [RequiresHostExtension(typeof(Guid), "key")]
        private class Application : EmptyApplication
        {
        }

        private class TestHostExtensions : IVostokHostExtensions
        {
            private readonly Dictionary<Type, object> items = new Dictionary<Type, object>();
            private readonly Dictionary<(Type, string), object> keyedItems = new Dictionary<(Type, string), object>();

            public void Add<TExtension>(TExtension extension)
                => items[typeof(TExtension)] = extension;

            public void Add<TExtension>(string key, TExtension extension)
                => keyedItems[(typeof(TExtension), key)] = extension;

            public TExtension Get<TExtension>()
                => (TExtension) items[typeof(TExtension)];

            public TExtension Get<TExtension>(string key)
                => (TExtension) keyedItems[(typeof(TExtension), key)];

            public bool TryGet<TExtension>(out TExtension result)
            {
                var found = items.TryGetValue(typeof(TExtension), out var item);
                result = found ? (TExtension)item : default;
                return found;
            }

            public bool TryGet<TExtension>(string key, out TExtension result)
            {
                var found = keyedItems.TryGetValue((typeof(TExtension), key), out var item);
                result = found ? (TExtension)item : default;
                return found;
            }

            public IEnumerable<(Type, object)> GetAll() 
                => items.Select(pair => (pair.Key, pair.Value));

            public IEnumerable<(Type Type, object Extension, string Key)> GetAll(bool withoutKeys, bool withKeys)
            {
                var result = Enumerable.Empty<(Type, object, string)>();

                if (withoutKeys)
                    result = result.Concat(items.Select(pair => (pair.Key, pair.Value, default(string))));

                if (withKeys)
                    result = result.Concat(keyedItems.Select(pair => (pair.Key.Item1, pair.Value, pair.Key.Item2)));

                return result;
            }
        }
    }
}
