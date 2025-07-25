﻿// -----------------------------------------------------------------------
// <copyright file="ZeroExtend.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Ubiquity.NET.Llvm.Instructions
{
    /// <summary>Instruction to zero extend a value</summary>
    public sealed class ZeroExtend
        : Cast
    {
        internal ZeroExtend( LLVMValueRef valueRef )
            : base( valueRef )
        {
        }
    }
}
