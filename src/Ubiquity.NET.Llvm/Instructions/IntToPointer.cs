﻿// -----------------------------------------------------------------------
// <copyright file="IntToPointer.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Ubiquity.NET.Llvm.Instructions
{
    /// <summary>Instruction to convert an integer to a pointer type</summary>
    /// <seealso href="xref:llvm_langref#inttoptr-to-instruction">LLVM inttoptr .. to Instruction</seealso>
    public sealed class IntToPointer
        : Cast
    {
        internal IntToPointer( LLVMValueRef valueRef )
            : base( valueRef )
        {
        }
    }
}
