﻿// -----------------------------------------------------------------------
// <copyright file="InsertValue.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Ubiquity.NET.Llvm.Instructions
{
    /// <summary>Instruction to insert a value into a member field in an aggregate value</summary>
    /// <seealso href="xref:llvm_langref#insertvalue-instruction">LLVM insertvalue Instruction</seealso>
    public sealed class InsertValue
        : Instruction
    {
        internal InsertValue( LLVMValueRef valueRef )
            : base( valueRef )
        {
        }
    }
}
