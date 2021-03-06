﻿// -----------------------------------------------------------------------
// <copyright file="VectorType.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Llvm.NET.Interop;
using Llvm.NET.Properties;

using static Llvm.NET.Interop.NativeMethods;

// Interface+internal type matches file name
#pragma warning disable SA1649

namespace Llvm.NET.Types
{
    /// <summary>Interface for an LLVM vector type</summary>
    public interface IVectorType
        : ISequenceType
    {
        /// <summary>Gets the number of elements in the vector</summary>
        uint Size { get; }
    }

    internal class VectorType
        : SequenceType
        , IVectorType
    {
        public uint Size => LLVMGetVectorSize( TypeRefHandle );

        internal VectorType( LLVMTypeRef typeRef )
            : base( typeRef )
        {
            if( LLVMGetTypeKind( typeRef ) != LLVMTypeKind.LLVMVectorTypeKind )
            {
                throw new ArgumentException( Resources.Vector_type_reference_expected, nameof( typeRef ) );
            }
        }
    }
}
