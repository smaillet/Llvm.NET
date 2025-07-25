﻿// -----------------------------------------------------------------------
// <copyright file="AddressSpaceCast.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Ubiquity.NET.Llvm.Instructions
{
    /// <summary>Address space cast instruction</summary>
    /// <seealso href="xref:llvm_langref#addrspaceast-to-instruction">LLVM addrspacecast .. to</seealso>
    public sealed class AddressSpaceCast : Cast
    {
        internal AddressSpaceCast( LLVMValueRef valueRef )
            : base( valueRef )
        {
        }
    }
}
