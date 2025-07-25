﻿// -----------------------------------------------------------------------
// <copyright file="VectorType.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

// Interface+internal type matches file name
#pragma warning disable SA1649

using static Ubiquity.NET.Llvm.Interop.ABI.llvm_c.Core;

namespace Ubiquity.NET.Llvm.Types
{
    /// <summary>Interface for an LLVM vector type</summary>
    public interface IVectorType
        : ISequenceType
    {
        /// <summary>Gets the number of elements in the vector</summary>
        uint Size { get; }
    }

    internal sealed class VectorType
        : SequenceType
        , IVectorType
    {
        public uint Size => LLVMGetVectorSize( Handle );

        internal VectorType( LLVMTypeRef typeRef )
            : base( typeRef )
        {
            if(LLVMGetTypeKind( typeRef ) != LLVMTypeKind.LLVMVectorTypeKind)
            {
                throw new ArgumentException( Resources.Vector_type_reference_expected, nameof( typeRef ) );
            }
        }
    }
}
