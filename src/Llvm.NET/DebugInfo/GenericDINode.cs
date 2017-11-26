﻿// <copyright file="GenericDINode.cs" company=".NET Foundation">
// Copyright (c) .NET Foundation. All rights reserved.
// </copyright>

using Llvm.NET.Native;

namespace Llvm.NET.DebugInfo
{
    /// <summary>Generic tagged DWARF-like Metadata node</summary>
    public class GenericDINode
        : DINode
    {
        /// <summary>Gets the header for this node</summary>
        /// <remarks>
        /// The header is a, possibly empty, null separated string
        /// header that contains arbitrary fields.
        /// </remarks>
        public string Header => GetOperand<MDString>( 0 ).ToString( );

        internal GenericDINode( LLVMMetadataRef handle )
            : base( handle )
        {
        }
    }
}
