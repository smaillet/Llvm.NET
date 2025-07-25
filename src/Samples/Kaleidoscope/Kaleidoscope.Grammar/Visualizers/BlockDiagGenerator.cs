﻿// -----------------------------------------------------------------------
// <copyright file="BlockDiagGenerator.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.CodeDom.Compiler;
using System.IO;

using OpenSoftware.DgmlTools.Model;

namespace Kaleidoscope.Grammar
{
    /// <summary>Extension class to generate a blockdiag file from a DGML <see cref="DirectedGraph"/></summary>
    /// <remarks>
    /// <para>This isn't a generator in the same sense that the <see cref="DgmlGenerator"/> and
    /// <see cref="XDocumentListener"/> are. Rather it is an extension class that allows
    /// generating a blockdiag diagram, from the directed graph created by the DgmlGenerator.
    /// The resulting ".diag" file is convertible to SVG form for documentation.</para>
    /// <para>
    /// The generated diagrams include a numbered element for binary operator expressions
    /// to indicate the precedence value that is dynamically evaluated for the expression.
    /// This is particularly useful for debugging custom operator precedence problems.
    /// </para>
    /// </remarks>
    /// <seealso href="http://blockdiag.com"/>
    public static class BlockDiagGenerator
    {
        public static void WriteAsBlockDiag( this DirectedGraph graph, TextWriter writer )
        {
            ArgumentNullException.ThrowIfNull( graph );
            ArgumentNullException.ThrowIfNull( writer );

            using var indentedWriter = new IndentedTextWriter( writer, "    " );

            indentedWriter.WriteLine( "blockdiag" );
            indentedWriter.WriteLine( '{' );
            ++indentedWriter.Indent;
            indentedWriter.WriteLine( "default_shape = roundedbox" );
            indentedWriter.WriteLine( "orientation = portrait" );

            indentedWriter.WriteLineNoTabs( string.Empty );
            indentedWriter.WriteLine( "// Nodes" );
            foreach(var node in graph.Nodes)
            {
                indentedWriter.Write( "N{0} [label= \"{1}\"", node.Id, node.Label );
                if(node.Properties.TryGetValue( "Precedence", out object? precedence ))
                {
                    indentedWriter.Write( ", numbered = {0}", precedence );
                }

                if(node.Category == "Terminal")
                {
                    indentedWriter.Write( ", shape = circle" );
                }

                indentedWriter.WriteLine( "];" );
            }

            indentedWriter.WriteLineNoTabs( string.Empty );
            indentedWriter.WriteLine( "// Edges" );
            foreach(var link in graph.Links)
            {
                indentedWriter.WriteLine( "N{0} -> N{1}", link.Source, link.Target );
            }

            --indentedWriter.Indent;
            indentedWriter.WriteLine( '}' );
        }
    }
}
