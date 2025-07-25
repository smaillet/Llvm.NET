﻿// -----------------------------------------------------------------------
// <copyright file="UnaryOperator.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Ubiquity.NET.Llvm.Instructions
{
    /// <summary>Base class for a unary operator</summary>
    public sealed class UnaryOperator
        : Instruction
    {
        internal UnaryOperator( LLVMValueRef valueRef )
            : base( valueRef )
        {
        }
    }
}
