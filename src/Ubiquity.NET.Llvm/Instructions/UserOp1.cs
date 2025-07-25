﻿// -----------------------------------------------------------------------
// <copyright file="UserOp1.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Ubiquity.NET.Llvm.Instructions
{
    /// <summary>Custom operator that can be used in LLVM transform passes but should be removed before target instruction selection</summary>
    public sealed class UserOp1
        : Instruction
    {
        internal UserOp1( LLVMValueRef valueRef )
            : base( valueRef )
        {
        }
    }
}
