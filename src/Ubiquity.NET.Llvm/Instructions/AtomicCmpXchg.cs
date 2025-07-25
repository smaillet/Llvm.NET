﻿// -----------------------------------------------------------------------
// <copyright file="AtomicCmpXchg.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using static Ubiquity.NET.Llvm.Interop.ABI.llvm_c.Core;

namespace Ubiquity.NET.Llvm.Instructions
{
    /// <summary>Atomic Compare and Exchange instruction</summary>
    /// <seealso href="xref:llvm_langref#cmpxchg-instruction">LLVM cmpxchg instruction</seealso>
    public sealed class AtomicCmpXchg
        : Instruction
    {
        /// <summary>Gets or sets a value indicating whether this instruction is weak or not</summary>
        public bool IsWeak
        {
            get => LLVMGetWeak( Handle );
            set => LLVMSetWeak( Handle, value );
        }

        internal AtomicCmpXchg( LLVMValueRef valueRef )
            : base( valueRef )
        {
        }
    }
}
