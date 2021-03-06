﻿// -----------------------------------------------------------------------
// <copyright file="IntToPointer.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Llvm.NET.Interop;

namespace Llvm.NET.Instructions
{
    /// <summary>Instruction to convert an integer to a pointer type</summary>
    /// <seealso href="xref:llvm_langref#inttoptr-to-instruction">LLVM inttoptr .. to Instruction</seealso>
    public class IntToPointer
        : Cast
    {
        internal IntToPointer( LLVMValueRef valueRef )
            : base( valueRef )
        {
        }
    }
}
