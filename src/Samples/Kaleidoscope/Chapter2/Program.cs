﻿// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Ubiquity.NET.Runtime.Utils;

namespace Kaleidoscope.Chapter2
{
    public static class Program
    {
        #region Main

        /// <summary>C# version of the LLVM Kaleidoscope language tutorial (Chapter 2)</summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of the program</returns>
        public static async Task Main( )
        {
            var repl = new ReplEngine( );

            using CancellationTokenSource cts = new();
            Console.CancelKeyPress += ( _, e ) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            string helloMsg = $"Ubiquity.NET.Llvm Kaleidoscope Parse evaluator - {repl.LanguageFeatureLevel}";
            Console.Title = $"{Assembly.GetExecutingAssembly().GetName()}: {helloMsg}";
            Console.WriteLine( helloMsg );
            await repl.Run( Console.In, new Visualizer( VisualizationKind.All ), cts.Token );

            Console.WriteLine();
            Console.WriteLine( "good bye!" );
        }
        #endregion
    }
}
