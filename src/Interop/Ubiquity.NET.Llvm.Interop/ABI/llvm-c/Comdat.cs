// -----------------------------------------------------------------------
// <copyright file="Comdat.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Ubiquity.NET.Llvm.Interop.ABI.llvm_c
{
    public enum LLVMComdatSelectionKind
        : Int32
    {
        LLVMAnyComdatSelectionKind = 0,
        LLVMExactMatchComdatSelectionKind = 1,
        LLVMLargestComdatSelectionKind = 2,
        LLVMNoDeduplicateComdatSelectionKind = 3,
        LLVMSameSizeComdatSelectionKind = 4,
    }

    public static partial class Comdat
    {
        [LibraryImport( LibraryName )]
        [UnmanagedCallConv( CallConvs = [ typeof( CallConvCdecl ) ] )]
        public static unsafe partial LLVMComdatRef LLVMGetOrInsertComdat( LLVMModuleRef M, LazyEncodedString Name );

        [LibraryImport( LibraryName )]
        [UnmanagedCallConv( CallConvs = [ typeof( CallConvCdecl ) ] )]
        public static unsafe partial LLVMComdatRef LLVMGetComdat( LLVMValueRef V );

        [LibraryImport( LibraryName )]
        [UnmanagedCallConv( CallConvs = [ typeof( CallConvCdecl ) ] )]
        public static unsafe partial void LLVMSetComdat( LLVMValueRef V, LLVMComdatRef C );

        [LibraryImport( LibraryName )]
        [UnmanagedCallConv( CallConvs = [ typeof( CallConvCdecl ) ] )]
        public static unsafe partial LLVMComdatSelectionKind LLVMGetComdatSelectionKind( LLVMComdatRef C );

        [LibraryImport( LibraryName )]
        [UnmanagedCallConv( CallConvs = [ typeof( CallConvCdecl ) ] )]
        public static unsafe partial void LLVMSetComdatSelectionKind( LLVMComdatRef C, LLVMComdatSelectionKind Kind );
    }
}
