using System;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Requirements
{
    /// <summary>
    /// Denotes, that host must provide <see cref="IVostokHostingEnvironment.Port"/> to the application.
    /// </summary>
    [PublicAPI]
    public class VostokHostPortRequiredAttribute : Attribute
    {

    }
}