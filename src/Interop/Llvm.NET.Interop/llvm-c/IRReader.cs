// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 2.17941.31104.49410
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using System.Security;

namespace Llvm.NET.Interop
{
    public static partial class NativeMethods
    {
        /// <include file="IRReader.xml" path='LibLLVMAPI/Function[@name="LLVMParseIRInContext"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMStatus LLVMParseIRInContext( LLVMContextRef ContextRef, LLVMMemoryBufferRef MemBuf, out LLVMModuleRef OutM, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( DisposeMessageMarshaler ) )]out string OutMessage );

    }
}
