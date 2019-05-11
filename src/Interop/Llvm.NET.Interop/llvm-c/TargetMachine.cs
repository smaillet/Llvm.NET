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
    /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMCodeGenOptLevel"]/*[not(self::Item)]' />
    [GeneratedCode("LlvmBindingsGenerator","2.17941.31104.49410")]
    public enum LLVMCodeGenOptLevel : global::System.Int32
    {
        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMCodeGenOptLevel"]/Item[@name="LLVMCodeGenLevelNone"]/*' />
        LLVMCodeGenLevelNone = 0,
        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMCodeGenOptLevel"]/Item[@name="LLVMCodeGenLevelLess"]/*' />
        LLVMCodeGenLevelLess = 1,
        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMCodeGenOptLevel"]/Item[@name="LLVMCodeGenLevelDefault"]/*' />
        LLVMCodeGenLevelDefault = 2,
        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMCodeGenOptLevel"]/Item[@name="LLVMCodeGenLevelAggressive"]/*' />
        LLVMCodeGenLevelAggressive = 3,
    }

    /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMRelocMode"]/*[not(self::Item)]' />
    [GeneratedCode("LlvmBindingsGenerator","2.17941.31104.49410")]
    public enum LLVMRelocMode : global::System.Int32
    {
        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMRelocMode"]/Item[@name="LLVMRelocDefault"]/*' />
        LLVMRelocDefault = 0,
        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMRelocMode"]/Item[@name="LLVMRelocStatic"]/*' />
        LLVMRelocStatic = 1,
        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMRelocMode"]/Item[@name="LLVMRelocPIC"]/*' />
        LLVMRelocPIC = 2,
        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMRelocMode"]/Item[@name="LLVMRelocDynamicNoPic"]/*' />
        LLVMRelocDynamicNoPic = 3,
        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMRelocMode"]/Item[@name="LLVMRelocROPI"]/*' />
        LLVMRelocROPI = 4,
        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMRelocMode"]/Item[@name="LLVMRelocRWPI"]/*' />
        LLVMRelocRWPI = 5,
        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMRelocMode"]/Item[@name="LLVMRelocROPI_RWPI"]/*' />
        LLVMRelocROPI_RWPI = 6,
    }

    /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMCodeModel"]/*[not(self::Item)]' />
    [GeneratedCode("LlvmBindingsGenerator","2.17941.31104.49410")]
    public enum LLVMCodeModel : global::System.Int32
    {
        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMCodeModel"]/Item[@name="LLVMCodeModelDefault"]/*' />
        LLVMCodeModelDefault = 0,
        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMCodeModel"]/Item[@name="LLVMCodeModelJITDefault"]/*' />
        LLVMCodeModelJITDefault = 1,
        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMCodeModel"]/Item[@name="LLVMCodeModelTiny"]/*' />
        LLVMCodeModelTiny = 2,
        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMCodeModel"]/Item[@name="LLVMCodeModelSmall"]/*' />
        LLVMCodeModelSmall = 3,
        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMCodeModel"]/Item[@name="LLVMCodeModelKernel"]/*' />
        LLVMCodeModelKernel = 4,
        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMCodeModel"]/Item[@name="LLVMCodeModelMedium"]/*' />
        LLVMCodeModelMedium = 5,
        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMCodeModel"]/Item[@name="LLVMCodeModelLarge"]/*' />
        LLVMCodeModelLarge = 6,
    }

    /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMCodeGenFileType"]/*[not(self::Item)]' />
    [GeneratedCode("LlvmBindingsGenerator","2.17941.31104.49410")]
    public enum LLVMCodeGenFileType : global::System.Int32
    {
        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMCodeGenFileType"]/Item[@name="LLVMAssemblyFile"]/*' />
        LLVMAssemblyFile = 0,
        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Enumeration[@name="LLVMCodeGenFileType"]/Item[@name="LLVMObjectFile"]/*' />
        LLVMObjectFile = 1,
    }

    public static partial class NativeMethods
    {
        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Function[@name="LLVMGetFirstTarget"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMTargetRef LLVMGetFirstTarget(  );

        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Function[@name="LLVMGetNextTarget"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMTargetRef LLVMGetNextTarget( LLVMTargetRef T );

        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Function[@name="LLVMGetTargetFromName"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMTargetRef LLVMGetTargetFromName( [MarshalAs( UnmanagedType.LPStr )]string Name );

        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Function[@name="LLVMGetTargetFromTriple"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMStatus LLVMGetTargetFromTriple( [MarshalAs( UnmanagedType.LPStr )]string Triple, out LLVMTargetRef T, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( DisposeMessageMarshaler ) )]out string ErrorMessage );

        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Function[@name="LLVMGetTargetName"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        [return: MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( AliasStringMarshaler ) )]
        public static extern string LLVMGetTargetName( LLVMTargetRef T );

        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Function[@name="LLVMGetTargetDescription"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        [return: MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( AliasStringMarshaler ) )]
        public static extern string LLVMGetTargetDescription( LLVMTargetRef T );

        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Function[@name="LLVMTargetHasJIT"]/*' />
        [return: MarshalAs( UnmanagedType.Bool )]
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern bool LLVMTargetHasJIT( LLVMTargetRef T );

        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Function[@name="LLVMTargetHasTargetMachine"]/*' />
        [return: MarshalAs( UnmanagedType.Bool )]
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern bool LLVMTargetHasTargetMachine( LLVMTargetRef T );

        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Function[@name="LLVMTargetHasAsmBackend"]/*' />
        [return: MarshalAs( UnmanagedType.Bool )]
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern bool LLVMTargetHasAsmBackend( LLVMTargetRef T );

        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Function[@name="LLVMCreateTargetMachine"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMTargetMachineRef LLVMCreateTargetMachine( LLVMTargetRef T, [MarshalAs( UnmanagedType.LPStr )]string Triple, [MarshalAs( UnmanagedType.LPStr )]string CPU, [MarshalAs( UnmanagedType.LPStr )]string Features, LLVMCodeGenOptLevel Level, LLVMRelocMode Reloc, LLVMCodeModel CodeModel );

        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Function[@name="LLVMGetTargetMachineTarget"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMTargetRef LLVMGetTargetMachineTarget( LLVMTargetMachineRef T );

        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Function[@name="LLVMGetTargetMachineTriple"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        [return: MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( DisposeMessageMarshaler ) )]
        public static extern string LLVMGetTargetMachineTriple( LLVMTargetMachineRef T );

        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Function[@name="LLVMGetTargetMachineCPU"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        [return: MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( DisposeMessageMarshaler ) )]
        public static extern string LLVMGetTargetMachineCPU( LLVMTargetMachineRef T );

        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Function[@name="LLVMGetTargetMachineFeatureString"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        [return: MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( DisposeMessageMarshaler ) )]
        public static extern string LLVMGetTargetMachineFeatureString( LLVMTargetMachineRef T );

        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Function[@name="LLVMCreateTargetDataLayout"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMTargetDataRef LLVMCreateTargetDataLayout( LLVMTargetMachineRef T );

        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Function[@name="LLVMSetTargetMachineAsmVerbosity"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMSetTargetMachineAsmVerbosity( LLVMTargetMachineRef T, [MarshalAs( UnmanagedType.Bool )]bool VerboseAsm );

        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Function[@name="LLVMTargetMachineEmitToFile"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMStatus LLVMTargetMachineEmitToFile( LLVMTargetMachineRef T, LLVMModuleRef M, [MarshalAs( UnmanagedType.LPStr )]string Filename, LLVMCodeGenFileType codegen, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( DisposeMessageMarshaler ) )]out string ErrorMessage );

        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Function[@name="LLVMTargetMachineEmitToMemoryBuffer"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern LLVMStatus LLVMTargetMachineEmitToMemoryBuffer( LLVMTargetMachineRef T, LLVMModuleRef M, LLVMCodeGenFileType codegen, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( DisposeMessageMarshaler ) )]out string ErrorMessage, out LLVMMemoryBufferRef OutMemBuf );

        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Function[@name="LLVMGetDefaultTargetTriple"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        unsafe public static extern sbyte* LLVMGetDefaultTargetTriple(  );

        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Function[@name="LLVMNormalizeTargetTriple"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        unsafe public static extern sbyte* LLVMNormalizeTargetTriple( [MarshalAs( UnmanagedType.LPStr )]string triple );

        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Function[@name="LLVMGetHostCPUName"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        unsafe public static extern sbyte* LLVMGetHostCPUName(  );

        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Function[@name="LLVMGetHostCPUFeatures"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        unsafe public static extern sbyte* LLVMGetHostCPUFeatures(  );

        /// <include file="TargetMachine.xml" path='LibLLVMAPI/Function[@name="LLVMAddAnalysisPasses"]/*' />
        [SuppressUnmanagedCodeSecurity]
        [DllImport( LibraryPath, CallingConvention=global::System.Runtime.InteropServices.CallingConvention.Cdecl )]
        public static extern void LLVMAddAnalysisPasses( LLVMTargetMachineRef T, LLVMPassManagerRef PM );

    }
}
