// -----------------------------------------------------------------------
// <copyright file="Use.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using static Ubiquity.NET.Llvm.Interop.ABI.llvm_c.Core;

namespace Ubiquity.NET.Llvm.Values
{
    /// <summary>LLVM Use, which is essentially a tuple of the <see cref="User"/> and the <see cref="Value"/> used</summary>
    /// <remarks>
    /// A Use in LLVM forms a link in a directed graph of dependencies for values.
    /// </remarks>
    public class Use
    {
        /// <summary>Gets the <see cref="User"/> of this <see cref="Use"/></summary>
        public User User => Value.FromHandle<User>( LLVMGetUser( OpaqueHandle ).ThrowIfInvalid() )!;

        /// <summary>Gets the <see cref="Value"/> used</summary>
        public Value Value => Value.FromHandle( LLVMGetUsedValue( OpaqueHandle ).ThrowIfInvalid() )!;

        /// <summary>Initializes a new instance of the <see cref="Use"/> class from low level LLVM <see cref="LLVMUseRef"/></summary>
        /// <param name="useRef">LLVM raw reference</param>
        internal Use( LLVMUseRef useRef )
        {
            OpaqueHandle = useRef;
        }

        private readonly LLVMUseRef OpaqueHandle;
    }
}
