// -----------------------------------------------------------------------
// <copyright file="ErrorHandling.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Ubiquity.NET.Llvm.Interop.ABI.llvm_c
{
    // Misplaced using directive; It isn't misplaced - tooling is too brain dead to know the difference between an alias and a using directive
#pragma warning disable IDE0065, SA1200, SA1135
    using unsafe LLVMFatalErrorHandler = delegate* unmanaged[Cdecl]< byte* /*Reason*/, void /*retVal*/>;
#pragma warning restore IDE0065, SA1200, SA1135

    public static partial class ErrorHandling
    {
        [LibraryImport( LibraryName )]
        [UnmanagedCallConv( CallConvs = [ typeof( CallConvCdecl ) ] )]
        public static unsafe partial void LLVMInstallFatalErrorHandler( LLVMFatalErrorHandler Handler );

        [LibraryImport( LibraryName )]
        [UnmanagedCallConv( CallConvs = [ typeof( CallConvCdecl ) ] )]
        public static unsafe partial void LLVMResetFatalErrorHandler( );

        [LibraryImport( LibraryName )]
        [UnmanagedCallConv( CallConvs = [ typeof( CallConvCdecl ) ] )]
        public static unsafe partial void LLVMEnablePrettyStackTrace( );
    }
}
