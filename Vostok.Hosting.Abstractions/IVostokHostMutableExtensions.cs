using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions
{
    [PublicAPI]
    public interface IVostokHostMutableExtensions : IVostokHostExtensions
    {
        /// <summary>
        /// Registers an extension of type <typeparamref name="TExtension"/> and given value.
        /// </summary>
        void Add<TExtension>(TExtension extension);

        /// <summary>
        /// Registers an extension of type <typeparamref name="TExtension"/>, given <paramref name="key"/> and given value.
        /// </summary>
        void Add<TExtension>([NotNull] string key, TExtension extension);
    }
}
