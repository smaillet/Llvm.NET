﻿// -----------------------------------------------------------------------
// <copyright file="DITemplateValueParameter.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Llvm.NET.Interop;

namespace Llvm.NET.DebugInfo
{
    /// <summary>Template Value parameter</summary>
    /// <seealso href="xref:llvm_langref#ditemplatevalueparameter">LLVM DITemplateValueParameter</seealso>
    public class DITemplateValueParameter
        : DITemplateParameter
    {
        /// <summary>Gets the value of the parameter as Metadata</summary>
        /// <typeparam name="T">Metadata type of the value to get</typeparam>
        /// <returns>Value or <see langword="null"/> if the value is not convertible to <typeparamref name="T"/></returns>
        public T GetValue<T>( )
            where T : LlvmMetadata
        {
            return GetOperand<T>( 2 );
        }

        internal DITemplateValueParameter( LLVMMetadataRef handle )
            : base( handle )
        {
        }
    }
}
