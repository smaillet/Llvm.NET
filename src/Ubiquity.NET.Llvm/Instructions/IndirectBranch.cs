﻿// -----------------------------------------------------------------------
// <copyright file="IndirectBranch.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Ubiquity.NET.Llvm.Instructions
{
    /// <summary>Instruction to perform an indirect branch to a block within the current function</summary>
    /// <remarks>The address of the branch must come from a <see cref="BlockAddress"/> constant</remarks>
    /// <seealso href="xref:llvm_langref#indirectbr-instruction">LLVM indirectbr Instruction</seealso>
    public sealed class IndirectBranch
        : Terminator
    {
        internal IndirectBranch( LLVMValueRef valueRef )
            : base( valueRef )
        {
        }
    }
}
