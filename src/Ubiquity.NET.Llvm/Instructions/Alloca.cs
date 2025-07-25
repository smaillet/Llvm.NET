﻿// -----------------------------------------------------------------------
// <copyright file="Alloca.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using static Ubiquity.NET.Llvm.Interop.ABI.llvm_c.Core;

namespace Ubiquity.NET.Llvm.Instructions
{
    /// <summary>Alloca instruction for allocating stack space</summary>
    /// <remarks>
    /// LLVM Mem2Reg pass will convert alloca locations to register for the
    /// entry block to the maximum extent possible.
    /// </remarks>
    /// <seealso href="xref:llvm_langref#alloca-instruction">LLVM alloca</seealso>
    public sealed class Alloca
        : UnaryInstruction
    {
        /// <summary>Gets the type of the alloca element</summary>
        /// <remarks>
        /// The <see cref="Ubiquity.NET.Llvm.Values.Value.NativeType"/> of an <see cref="Alloca"/>
        /// is always a pointer type, this provides the ElementType (e.g. the pointee type)
        /// for the alloca.
        /// </remarks>
        public ITypeRef ElementType => LLVMGetAllocatedType( Handle ).CreateType();

        internal Alloca( LLVMValueRef valueRef )
            : base( valueRef )
        {
        }
    }
}
