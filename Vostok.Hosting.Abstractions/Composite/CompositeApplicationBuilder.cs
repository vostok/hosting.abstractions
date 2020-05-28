using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Composite
{
    internal class CompositeApplicationBuilder : ICompositeApplicationBuilder
    {
        private readonly List<IVostokApplication> applications = new List<IVostokApplication>();
        private readonly CompositeApplicationSettings settings = new CompositeApplicationSettings();

        public static (IReadOnlyList<IVostokApplication> apps, CompositeApplicationSettings settings) Build(
            [NotNull] Action<ICompositeApplicationBuilder> configure)
        {
            if (configure == null)
                throw new ArgumentNullException(nameof(configure));

            var builder = new CompositeApplicationBuilder();

            configure(builder);

            builder.ValidateApplications();

            return (builder.applications, builder.settings);
        }

        public ICompositeApplicationBuilder AddApplication(IVostokApplication application)
        {
            applications.Add(application ?? throw new ArgumentNullException(nameof(application)));
            return this;
        }

        public ICompositeApplicationBuilder CustomizeSettings(Action<CompositeApplicationSettings> customization)
        {
            (customization ?? throw new ArgumentNullException(nameof(customization)))(settings);
            return this;
        }

        private void ValidateApplications()
        {
            if (applications.Count == 0)
                throw new ArgumentException($"{nameof(CompositeApplication)} must contain at least one component application.", nameof(applications));
        }
    }
}
