﻿// -----------------------------------------------------------------------
// <copyright file="ConstantStruct.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Ubiquity.NET.Llvm.Values
{
    /// <summary>Constant Structure</summary>
    public sealed class ConstantStruct
        : ConstantAggregate
    {
        internal ConstantStruct( LLVMValueRef valueRef )
            : base( valueRef )
        {
        }
    }
}
