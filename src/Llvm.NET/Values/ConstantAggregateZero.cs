﻿// -----------------------------------------------------------------------
// <copyright file="ConstantAggregateZero.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Llvm.NET.Interop;

namespace Llvm.NET.Values
{
    /// <summary>Constant aggregate of value 0</summary>
    public class ConstantAggregateZero
        : ConstantData
    {
        internal ConstantAggregateZero( LLVMValueRef valueRef )
            : base( valueRef )
        {
        }
    }
}
