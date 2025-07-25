﻿// -----------------------------------------------------------------------
// <copyright file="ConstantDataArray.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Ubiquity.NET.Llvm.Values
{
    /// <summary>Array of constant data</summary>
    public class ConstantDataArray
        : ConstantDataSequential
    {
        internal ConstantDataArray( LLVMValueRef valueRef )
            : base( valueRef )
        {
        }
    }
}
