﻿// -----------------------------------------------------------------------
// <copyright file="IParseErrorListener.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Ubiquity.NET.Runtime.Utils
{
    /// <summary>Interface for a generic error listener</summary>
    public interface IParseErrorListener
    {
        /// <summary>Process a single SyntaxError found during parse</summary>
        /// <param name="syntaxError">Error found</param>
        /// <remarks>
        /// Implementation should not assume that calling this is "terminal" for the
        /// parse. Many parsers are able to "recover" from syntax errors (and may
        /// even do so correctly) but the original source still contains the erroneous
        /// input.
        /// </remarks>
        void SyntaxError( SyntaxError syntaxError );
    }
}
