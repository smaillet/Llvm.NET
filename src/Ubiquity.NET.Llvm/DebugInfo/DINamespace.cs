﻿// -----------------------------------------------------------------------
// <copyright file="DINamespace.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Ubiquity.NET.Llvm.DebugInfo
{
    /// <summary>Debug information namespace scope</summary>
    /// <seealso href="xref:llvm_langref#dinamespace"/>
    public class DINamespace
        : DIScope
    {
        /* TODO: non-operand properties
        public bool ExportSymbols => LLVMDINamespaceGetExportSymbols( NativeHandle );
        */

        /// <inheritdoc/>
        public override DIScope? Scope => GetOperand<DIScope>( 1 );

        /// <inheritdoc/>
        public override LazyEncodedString Name => GetOperandString( 2 );

        internal DINamespace( LLVMMetadataRef handle )
            : base( handle )
        {
        }
    }
}
