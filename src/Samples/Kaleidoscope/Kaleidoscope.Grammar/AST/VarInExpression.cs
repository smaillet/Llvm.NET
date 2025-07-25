﻿// -----------------------------------------------------------------------
// <copyright file="VarInExpression.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Text;

using Ubiquity.NET.Runtime.Utils;

namespace Kaleidoscope.Grammar.AST
{
    public sealed class VarInExpression
        : AstNode
        , IExpression
    {
        public VarInExpression( SourceLocation location, IEnumerable<LocalVariableDeclaration> localVariables, IExpression body )
            : base( location )
        {
            LocalVariables = localVariables;
            Body = body;
        }

        public IEnumerable<LocalVariableDeclaration> LocalVariables { get; }

        public IExpression Body { get; }

        public override TResult? Accept<TResult>( IAstVisitor<TResult> visitor )
            where TResult : default
        {
            return visitor is IKaleidoscopeAstVisitor<TResult> klsVisitor
                   ? klsVisitor.Visit( this )
                   : visitor.Visit( this );
        }

        public override TResult? Accept<TResult, TArg>( IAstVisitor<TResult, TArg> visitor, ref readonly TArg arg )
            where TResult : default
        {
            return visitor is IKaleidoscopeAstVisitor<TResult, TArg> klsVisitor
                   ? klsVisitor.Visit( this, in arg )
                   : visitor.Visit( this, in arg );
        }

        public override IEnumerable<IAstNode> Children
        {
            get
            {
                foreach(var local in LocalVariables)
                {
                    yield return local;
                }

                yield return Body;
            }
        }

        public override string ToString( )
        {
            var bldr = new StringBuilder( "VarIn{" );
            bldr.AppendJoin( ',', LocalVariables )
                .Append( "}(" )
                .Append( Body )
                .Append( ')' );
            return bldr.ToString();
        }
    }
}
