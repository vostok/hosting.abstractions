using System;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Composite
{
    [PublicAPI]
    public interface ICompositeApplicationBuilder
    {
        /// <summary>
        /// <para>Adds given <paramref name="application"/> to the <see cref="CompositeApplication"/> being built.</para>
        /// <para>Note that the order of the subapplications might be of importance during initialization.</para>
        /// </summary>
        [NotNull]
        ICompositeApplicationBuilder AddApplication([NotNull] IVostokApplication application);

        /// <summary>
        /// Applies given <paramref name="customization"/> to the settings used to construct the instance of <see cref="CompositeApplication"/>.
        /// </summary>
        [NotNull]
        ICompositeApplicationBuilder CustomizeSettings([NotNull] Action<CompositeApplicationSettings> customization);
    }
}
