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

        public override string ToString() => $"{Component}/{Name}";

        #region Parsing

        public static bool TryParse(string input, out DiagnosticEntry entry)
        {
            entry = null;

            if (string.IsNullOrWhiteSpace(input))
                return false;

            var parts = input.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
                return false;

            entry = new DiagnosticEntry(parts[0], parts[1]);
            return true;
        } 

        #endregion

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
