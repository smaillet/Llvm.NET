﻿// -----------------------------------------------------------------------
// <copyright file="UnexpectedNullHandleException.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Ubiquity.NET.InteropHelpers
{
    /// <summary>Exception thrown when an underlying Native API `handle` is unexpectedly <see langword="null"/></summary>
    /// <remarks>
    /// This is generally a non-recoverable error state where the underlying API library is in an inconsistent
    /// or otherwise unexpected state.
    /// </remarks>
    public sealed class UnexpectedNullHandleException
        : InvalidOperationException
    {
        /// <summary>Initializes a new instance of the <see cref="UnexpectedNullHandleException"/> class.</summary>
        /// <param name="message">Exception error message</param>
        public UnexpectedNullHandleException( string message )
            : base( message )
        {
        }

        /// <summary>Initializes a new instance of the <see cref="UnexpectedNullHandleException"/> class.</summary>
        /// <param name="message">Exception error message</param>
        /// <param name="innerException">Inner Exception</param>
        public UnexpectedNullHandleException( string message, Exception innerException )
            : base( message, innerException )
        {
        }

        /// <summary>Initializes a new instance of the <see cref="UnexpectedNullHandleException"/> class.</summary>
        public UnexpectedNullHandleException( )
        {
        }
    }
}
