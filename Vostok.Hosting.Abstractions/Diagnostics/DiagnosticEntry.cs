using System;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Diagnostics
{
    /// <summary>
    /// <see cref="DiagnosticEntry"/> serves as the unique key of any particular section of diagnostic information.
    /// </summary>
    [PublicAPI]
    public class DiagnosticEntry
    {
        public DiagnosticEntry([NotNull] string component, [NotNull] string name)
        {
            Component = component ?? throw new ArgumentNullException(nameof(component));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// The component providing the info (library / application).
        /// </summary>
        [NotNull]
        public string Component { get; }

        /// <summary>
        /// Name of the entry within its <see cref="Component"/>.
        /// </summary>
        [NotNull]
        public string Name { get; }

        #region Equality members

        protected bool Equals(DiagnosticEntry other) =>
           string.Equals(Component, other.Component, StringComparison.OrdinalIgnoreCase) &&
           string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((DiagnosticEntry)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (StringComparer.OrdinalIgnoreCase.GetHashCode(Component) * 397) ^ StringComparer.OrdinalIgnoreCase.GetHashCode(Name);
            }
        } 

        #endregion
    }
}
