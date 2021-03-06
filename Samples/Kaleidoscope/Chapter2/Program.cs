﻿// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Reactive.Linq;
using System.Reflection;
using Kaleidoscope.Grammar;
using Kaleidoscope.Grammar.AST;
using Kaleidoscope.Runtime;

namespace Kaleidoscope.Chapter2
{
    public static class Program
    {
        // Using language level that includes the complete set for exploration of pare trees and AST
        private const LanguageLevel LanguageFeatureLevel = LanguageLevel.MutableVariables;

        /// <summary>C# version of the LLVM Kaleidoscope language tutorial</summary>
        public static void Main( /*string[ ] args*/ )
        {
            string helloMsg = $"Llvm.NET Kaleidoscope Explorer - {LanguageFeatureLevel}";
            Console.Title = $"{Assembly.GetExecutingAssembly( ).GetName( )}: {helloMsg}";
            Console.WriteLine( helloMsg );

            #region GeneratorLoop
            var parser = new Parser( LanguageFeatureLevel );

            // Create Observable chain to parse input lines from console into AST Nodes
            var replSeq = parser.Parse( Console.In.ToObservableStatements( ShowPrompt ), ShowCodeGenError );

            // Subscribe to the sequence the sequence
            using( replSeq.Subscribe( ShowResults ) )
            {
            }

            Console.WriteLine( "Bye!" );
            #endregion
        }

        #region ShowPrompt
        private static void ShowPrompt(ReadyState state)
        {
            Console.Write( state == ReadyState.StartExpression ? "Ready>" : ">" );
        }
        #endregion

        #region Error Handling
        private static void ShowCodeGenError( CodeGeneratorException ex )
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine( ex.Message );
            Console.ForegroundColor = color;
        }
        #endregion

        #region ShowResults
        private static void ShowResults( IAstNode node )
        {
            Console.WriteLine( "Parsed {0}", node.GetType( ).Name );
        }
        #endregion
    }
}
