﻿// -----------------------------------------------------------------------
// <copyright file="DINodeArray.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Ubiquity.NET.Llvm.DebugInfo
{
    /// <summary>Array of <see cref="DINode"/> debug information nodes for use with <see cref="DIBuilder"/> methods</summary>
    /// <seealso cref="DIBuilder.GetOrCreateArray(System.Collections.Generic.IEnumerable{Ubiquity.NET.Llvm.DebugInfo.DINode})"/>
    [SuppressMessage( "Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This matches the wrapped native type" )]
    public class DINodeArray : TupleTypedArrayWrapper<DINode>
    {
        internal DINodeArray( MDTuple? tuple )
            : base( tuple )
        {
        }
    }
}
