﻿// -----------------------------------------------------------------------
// <copyright file="FailedPredicateErrorStrategy.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

using Antlr4.Runtime;

namespace Kaleidoscope.Grammar
{
    /// <summary>Error handling strategy for REPL scenarios</summary>
    /// <remarks>
    /// This strategy handles creating more meaningful messages on feature predicate failures.
    /// </remarks>
    internal class FailedPredicateErrorStrategy
        : DefaultErrorStrategy
    {
        protected override void ReportFailedPredicate( Antlr4.Runtime.Parser recognizer, FailedPredicateException e )
        {
            ArgumentNullException.ThrowIfNull( e );
            switch(e.Predicate)
            {
            case "FeatureControlFlow":
                NotifyErrorListeners( recognizer, "Control flow expressions not supported in this version of the language", e );
                break;

            case "FeatureConditionalExpr":
                NotifyErrorListeners( recognizer, "Conditional expressions not supported in this version of the language", e );
                break;

            case "FeatureMutableVars":
                NotifyErrorListeners( recognizer, "Mutable variables not supported in this version of the language", e );
                break;

            default:
                base.ReportFailedPredicate( recognizer, e );
                break;
            }
        }
    }
}
