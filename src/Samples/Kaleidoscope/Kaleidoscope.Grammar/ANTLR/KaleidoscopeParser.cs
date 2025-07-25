﻿// -----------------------------------------------------------------------
// <copyright file="KaleidoscopeParser.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

using Antlr4.Runtime;

using Ubiquity.NET.ANTLR.Utils;
using Ubiquity.NET.Runtime.Utils;

namespace Kaleidoscope.Grammar.ANTLR
{
    // partial class customization to the ANTLR generated class
    // This extends the individual parse tree context node types
    // so that labels in the grammar are unnecessary.
    internal partial class KaleidoscopeParser
    {
        public KaleidoscopeParser( ITokenStream tokenStream, DynamicRuntimeState globalState, IParseErrorListener? errorListener, bool useDiagnosticListener = false )
            : this( tokenStream )
        {
            ArgumentNullException.ThrowIfNull( globalState );

            GlobalState = globalState;
            if(errorListener != null)
            {
                RemoveErrorListeners();
                AddErrorListener( new ParseErrorListenerAdapter( errorListener ) );
            }

            if(globalState.LanguageLevel >= LanguageLevel.UserDefinedOperators)
            {
                AddParseListener( new KaleidoscopeUserOperatorListener( GlobalState ) );
            }

            if(useDiagnosticListener)
            {
                AddParseListener( new DebugTraceListener( this ) );
            }

            ErrorHandler = new FailedPredicateErrorStrategy();
        }

        /// <summary>Gets the Language level the application supports</summary>
        public LanguageLevel LanguageLevel => GlobalState.LanguageLevel;

        /// <summary>Gets or sets the dynamic state of the runtime for the parser</summary>
        /// <remarks>
        /// This provides the <see cref="LanguageLevel"/> along with the
        /// operators currently available, including user defined operators.
        /// </remarks>
        public DynamicRuntimeState GlobalState { get; set; }

        public bool FeatureControlFlow => IsFeatureEnabled( LanguageLevel.ControlFlow );

        public bool FeatureMutableVars => IsFeatureEnabled( LanguageLevel.MutableVariables );

        public bool FeatureUserOperators => IsFeatureEnabled( LanguageLevel.UserDefinedOperators );

        private bool IsFeatureEnabled( LanguageLevel feature ) => LanguageLevel >= feature;

        private bool IsPrefixOp( ) => GlobalState.IsPrefixOp( InputStream.LA( 1 ) );

        private int GetPrecedence( )
        {
            return GlobalState.GetPrecedence( InputStream.LA( 1 ) );
        }

        private int GetNextPrecedence( )
        {
            return GlobalState.GetNextPrecedence( InputStream.LA( -1 ) );
        }
    }
}
