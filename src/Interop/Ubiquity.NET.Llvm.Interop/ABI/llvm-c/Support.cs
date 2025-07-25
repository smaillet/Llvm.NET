// -----------------------------------------------------------------------
// <copyright file="Support.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

#if INCLUDE_LLVM_LIBRARY_SUPPORT_ABI

namespace Ubiquity.NET.Llvm.Interop
{
    // CONSIDER: These APIs are highly questionable in a managed environment. Should they even be published?
    public static partial class Support
    {
        [LibraryImport( NativeMethods.LibraryPath, StringMarshallingCustomType = typeof(ExecutionEncodingStringMarshaller) )]
        [UnmanagedCallConv( CallConvs = [typeof(CallConvCdecl)] )]
        [return: MarshalAs( UnmanagedType.Bool )]
        public static unsafe partial bool LLVMLoadLibraryPermanently( string Filename );

        [LibraryImport( NativeMethods.LibraryPath, StringMarshallingCustomType = typeof(ExecutionEncodingStringMarshaller))]
        [UnmanagedCallConv( CallConvs = [typeof(CallConvCdecl)] )]
        public static unsafe partial void LLVMParseCommandLineOptions( int argc, [In]string[] argv, string Overview );

        [LibraryImport( NativeMethods.LibraryPath, StringMarshallingCustomType = typeof(ExecutionEncodingStringMarshaller) )]
        [UnmanagedCallConv( CallConvs = [typeof(CallConvCdecl)] )]
        public static unsafe partial nint LLVMSearchForAddressOfSymbol( string symbolName );

        [LibraryImport( NativeMethods.LibraryPath, StringMarshallingCustomType = typeof(ExecutionEncodingStringMarshaller) )]
        [UnmanagedCallConv( CallConvs = [typeof(CallConvCdecl)] )]
        public static unsafe partial void LLVMAddSymbol( string symbolName, void* symbolValue );
    }
}
#endif
