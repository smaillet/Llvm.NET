﻿// -----------------------------------------------------------------------
// <copyright file="DILocalVariableArray.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Ubiquity.NET.Llvm.DebugInfo
{
    /// <summary>Array of <see cref="DILocalVariable"/> nodes for use with see <see cref="DIBuilder"/> methods</summary>
    [SuppressMessage( "Naming", "CA1710:Identifiers should have correct suffix", Justification = "Name matches underlying LLVM and is descriptive of what it is" )]
    public class DILocalVariableArray
        : TupleTypedArrayWrapper<DILocalVariable>
    {
        internal DILocalVariableArray( MDTuple tuple )
            : base( tuple )
        {
        }
    }
}
