﻿// -----------------------------------------------------------------------
// <copyright file="FPToSI.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Ubiquity.NET.Llvm.Instructions
{
    /// <summary>Instruction to convert a floating point value to a signed integer type</summary>
    /// <seealso href="xref:llvm_langref#fptosi-to-instruction">LLVM fptosi .. to Instruction</seealso>
    public sealed class FPToSI
        : Cast
    {
        internal FPToSI( LLVMValueRef valueRef )
            : base( valueRef )
        {
        }
    }
}
