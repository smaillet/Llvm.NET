﻿// -----------------------------------------------------------------------
// <copyright file="DITemplateValueParameter.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Ubiquity.NET.Llvm.DebugInfo
{
    /// <summary>Template Value parameter</summary>
    /// <seealso href="xref:llvm_langref#ditemplatevalueparameter">LLVM DITemplateValueParameter</seealso>
    public class DITemplateValueParameter
        : DITemplateParameter
    {
        /// <summary>Gets the value of the parameter as IrMetadata</summary>
        /// <typeparam name="T">IrMetadata type of the value to get</typeparam>
        /// <returns>Value or <see langword="null"/> if the value is not convertible to <typeparamref name="T"/></returns>
        public T GetValue<T>( )
            where T : IrMetadata
        {
            return GetOperand<T>( 2 ) ?? throw new InternalCodeGeneratorException( "Could not get a valid value from LLVM interop" );
        }

        internal DITemplateValueParameter( LLVMMetadataRef handle )
            : base( handle )
        {
        }
    }
}
