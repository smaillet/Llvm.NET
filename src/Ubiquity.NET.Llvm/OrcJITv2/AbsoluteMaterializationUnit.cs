﻿// -----------------------------------------------------------------------
// <copyright file="AbsoluteMaterializationUnit.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using static Ubiquity.NET.Llvm.Interop.ABI.llvm_c.Orc;

namespace Ubiquity.NET.Llvm.OrcJITv2
{
    /// <summary>Materialization unit for absolute symbols in an LLVM ORC v2 JIT</summary>
    public class AbsoluteMaterializationUnit
        : MaterializationUnit
    {
        /// <summary>Initializes a new instance of the <see cref="AbsoluteMaterializationUnit"/> class.</summary>
        /// <param name="absoluteSymbols">Absolute (pre-evaluated) symbols to add to the JIT</param>
        public AbsoluteMaterializationUnit( IReadOnlyCollection<KeyValuePair<SymbolStringPoolEntry, EvaluatedSymbol>> absoluteSymbols )
            : base( MakeHandle( absoluteSymbols ) )
        {
        }

        private static LLVMOrcMaterializationUnitRef MakeHandle( IReadOnlyCollection<KeyValuePair<SymbolStringPoolEntry, EvaluatedSymbol>> absoluteSymbols )
        {
            // make a native usable version of the array
            using IMemoryOwner<LLVMOrcCSymbolMapPair> nativeArrayOwner = absoluteSymbols.InitializeNativeCopy( );

            // pin the memory and call the native API
            using var nativeMemHandle = nativeArrayOwner.Memory.Pin();
            unsafe
            {
                return LLVMOrcAbsoluteSymbols( (LLVMOrcCSymbolMapPair*)nativeMemHandle.Pointer, checked((nuint)absoluteSymbols.Count) );
            }
        }
    }
}
