﻿using System;
using JetBrains.Annotations;

namespace Vostok.Hosting.Abstractions.Requirements
{
    /// <summary>
    /// Denotes that application requires the host to register an extension of type <see cref="Type"/> in <see cref="IVostokHostingEnvironment.HostExtensions"/>.
    /// </summary>
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
    public class RequiresHostExtension : Attribute
    {
        [NotNull]
        public readonly Type Type;

        public RequiresHostExtension([NotNull] Type type) =>
            Type = type ?? throw new ArgumentNullException(nameof(type));
    }
}