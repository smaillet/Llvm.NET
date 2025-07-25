// -----------------------------------------------------------------------
// <copyright file="ValueBindings.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

// Usually ordering applies, however in this case the ordering is by method name
// and sometimes contains a wrapper method on the low level to make use easier.
#pragma warning disable SA1202 // Elements should be ordered by access

namespace Ubiquity.NET.Llvm.Interop.ABI.libllvm_c
{
    public enum LibLLVMValueKind
        : Int32
    {
        FunctionKind = 0,
        GlobalAliasKind = 1,
        GlobalIFuncKind = 2,
        GlobalVariableKind = 3,
        BlockAddressKind = 4,
        ConstantExprKind = 5,
        DSOLocalEquivalentKind = 6,
        NoCFIValueKind = 7,
        ConstantPtrAuthKind = 8,
        ConstantArrayKind = 9,
        ConstantStructKind = 10,
        ConstantVectorKind = 11,
        UndefValueKind = 12,
        PoisonValueKind = 13,
        ConstantAggregateZeroKind = 14,
        ConstantDataArrayKind = 15,
        ConstantDataVectorKind = 16,
        ConstantIntKind = 17,
        ConstantFPKind = 18,
        ConstantTargetNoneKind = 19,
        ConstantPointerNullKind = 20,
        ConstantTokenNoneKind = 21,
        ArgumentKind = 22,
        BasicBlockKind = 23,
        MetadataAsValueKind = 24,
        InlineAsmKind = 25,
        MemoryUseKind = 26,
        MemoryDefKind = 27,
        MemoryPhiKind = 28,
        InstructionKind = 29,
        RetKind = 30,
        BrKind = 31,
        SwitchKind = 32,
        IndirectBrKind = 33,
        InvokeKind = 34,
        ResumeKind = 35,
        UnreachableKind = 36,
        CleanupRetKind = 37,
        CatchRetKind = 38,
        CatchSwitchKind = 39,
        CallBrKind = 40,
        FNegKind = 41,
        AddKind = 42,
        FAddKind = 43,
        SubKind = 44,
        FSubKind = 45,
        MulKind = 46,
        FMulKind = 47,
        UDivKind = 48,
        SDivKind = 49,
        FDivKind = 50,
        URemKind = 51,
        SRemKind = 52,
        FRemKind = 53,
        ShlKind = 54,
        LShrKind = 55,
        AShrKind = 56,
        AndKind = 57,
        OrKind = 58,
        XorKind = 59,
        AllocaKind = 60,
        LoadKind = 61,
        StoreKind = 62,
        GetElementPtrKind = 63,
        FenceKind = 64,
        AtomicCmpXchgKind = 65,
        AtomicRMWKind = 66,
        TruncKind = 67,
        ZExtKind = 68,
        SExtKind = 69,
        FPToUIKind = 70,
        FPToSIKind = 71,
        UIToFPKind = 72,
        SIToFPKind = 73,
        FPTruncKind = 74,
        FPExtKind = 75,
        PtrToIntKind = 76,
        IntToPtrKind = 77,
        BitCastKind = 78,
        AddrSpaceCastKind = 79,
        CleanupPadKind = 80,
        CatchPadKind = 81,
        ICmpKind = 82,
        FCmpKind = 83,
        PHIKind = 84,
        CallKind = 85,
        SelectKind = 86,
        UserOp1Kind = 87,
        UserOp2Kind = 88,
        VAArgKind = 89,
        ExtractElementKind = 90,
        InsertElementKind = 91,
        ShuffleVectorKind = 92,
        ExtractValueKind = 93,
        InsertValueKind = 94,
        LandingPadKind = 95,
        FreezeKind = 96,
        ConstantFirstValKind = FunctionKind,
        ConstantLastValKind = ConstantTokenNoneKind,
        ConstantDataFirstValKind = UndefValueKind,
        ConstantDataLastValKind = ConstantTokenNoneKind,
        ConstantAggregateFirstValKind = ConstantArrayKind,
        ConstantAggregateLastValKind = ConstantVectorKind,
    }

    public static partial class ValueBindings
    {
        [LibraryImport( LibraryName )]
        [UnmanagedCallConv( CallConvs = [ typeof( CallConvCdecl ) ] )]
        [return: MarshalAs( UnmanagedType.Bool )]
        public static unsafe partial bool LibLLVMIsConstantZeroValue( LLVMValueRef valueRef );

        [LibraryImport( LibraryName )]
        [UnmanagedCallConv( CallConvs = [ typeof( CallConvCdecl ) ] )]
        public static unsafe partial void LibLLVMRemoveGlobalFromParent( LLVMValueRef valueRef );

        [LibraryImport( LibraryName )]
        [UnmanagedCallConv( CallConvs = [ typeof( CallConvCdecl ) ] )]
        public static unsafe partial LibLLVMValueKind LibLLVMGetValueKind( LLVMValueRef valueRef );

        [LibraryImport( LibraryName )]
        [UnmanagedCallConv( CallConvs = [ typeof( CallConvCdecl ) ] )]
        public static unsafe partial LLVMValueRef LibLLVMGetAliasee( LLVMValueRef Val );

        [LibraryImport( LibraryName )]
        [UnmanagedCallConv( CallConvs = [ typeof( CallConvCdecl ) ] )]
        public static unsafe partial UInt32 LibLLVMGetArgumentIndex( LLVMValueRef Val );

        [LibraryImport( LibraryName )]
        [UnmanagedCallConv( CallConvs = [ typeof( CallConvCdecl ) ] )]
        public static unsafe partial void LibLLVMGlobalVariableAddDebugExpression( LLVMValueRef globalVar, LLVMMetadataRef exp );

        [LibraryImport( LibraryName )]
        [UnmanagedCallConv( CallConvs = [ typeof( CallConvCdecl ) ] )]
        public static unsafe partial void LibLLVMFunctionAppendBasicBlock( LLVMValueRef function, LLVMBasicBlockRef block );

        [LibraryImport( LibraryName )]
        [UnmanagedCallConv( CallConvs = [ typeof( CallConvCdecl ) ] )]
        public static unsafe partial LLVMValueRef LibLLVMValueAsMetadataGetValue( LLVMMetadataRef vmd );

        [LibraryImport( LibraryName )]
        [UnmanagedCallConv( CallConvs = [ typeof( CallConvCdecl ) ] )]
        [return: MarshalAs( UnmanagedType.Bool )]
        public static unsafe partial bool LibLLVMIsConstantCString( LLVMValueRef C );

        [LibraryImport( LibraryName )]
        [UnmanagedCallConv( CallConvs = [ typeof( CallConvCdecl ) ] )]
        public static unsafe partial UInt32 LibLLVMGetConstantDataSequentialElementCount( LLVMValueRef C );

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static ReadOnlySpan<byte> LibLLVMGetConstantDataSequentialRawData( LLVMValueRef C )
        {
            unsafe
            {
                byte* p = LibLLVMGetConstantDataSequentialRawData(C, out nuint len);
                return new( p, checked((int)len) );
            }
        }

        [LibraryImport( LibraryName )]
        [UnmanagedCallConv( CallConvs = [ typeof( CallConvCdecl ) ] )]
        private static unsafe partial byte* LibLLVMGetConstantDataSequentialRawData( LLVMValueRef C, out nuint Length );

        [LibraryImport( LibraryName )]
        [UnmanagedCallConv( CallConvs = [ typeof( CallConvCdecl ) ] )]
        [return: MarshalAs( UnmanagedType.Bool )]
        public static unsafe partial bool LibLLVMHasDbgRecords( LLVMValueRef C );
    }
}
