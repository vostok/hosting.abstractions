using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Composite
{
    /// <summary>
    /// Options that govern behaviour of <see cref="CompositeApplication"/>s.
    /// </summary>
    [PublicAPI]
    public class CompositeApplicationSettings
    {
        /// <summary>
        /// <para>If enabled, subapplication are initialized concurrently.</para>
        /// <para>If disabled, subapplication are initialized sequentially in the order they were added.</para>
        /// <para>Default value is <c>false</c> (sequential initialization).</para>
        /// </summary>
        public bool UseParallelInitialization { get; set; }

        /// <summary>
        /// <para>If enabled, subapplication are executed concurrently.</para>
        /// <para>If disabled, subapplication are executed sequentially in the order they were added.</para>
        /// <para>Default value is <c>true</c> (parallel execution).</para>
        /// </summary>
        public bool UseParallelExecution { get; set; } = true;
    }
}
