﻿// -----------------------------------------------------------------------
// <copyright file="FPToUI.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Ubiquity.NET.Llvm.Instructions
{
    /// <summary>Instruction to convert a floating point value to an unsigned integer type</summary>
    /// <seealso href="xref:llvm_langref#fptoui-to-instruction">LLVM fptoui .. to Instruction</seealso>
    public sealed class FPToUI
        : Cast
    {
        internal FPToUI( LLVMValueRef valueRef )
            : base( valueRef )
        {
        }
    }
}
