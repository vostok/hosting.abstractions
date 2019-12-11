using System;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Requirements
{
    /// <summary>
    /// Denotes that host must provide a <see cref="IVostokHostingEnvironment.Port"/> to the application.
    /// </summary>
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Class)]
    public class RequiresPort : Attribute
    {

    }
}