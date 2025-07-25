﻿// -----------------------------------------------------------------------
// <copyright file="DebugBasicType.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Ubiquity.NET.Llvm.DebugInfo
{
    /// <summary>Debug information binding between an LLVM native <see cref="ITypeRef"/> and a <see cref="DIBasicType"/></summary>
    /// <remarks>
    /// This class provides a binding between an LLVM type and a corresponding <see cref="DIBasicType"/>.
    /// In LLVM all primitive types are unnamed and interned. That is, any use of an i8 is always the same
    /// type. However, at the source language level it is common to have named primitive types that map
    /// to the same underlying LLVM. For example, in C and C++ char maps to i8 but so does unsigned char
    /// (LLVM integral types don't have signed vs unsigned). This class is designed to handle this sort
    /// of one to many mapping of the lower level LLVM types to source level debugging types. Each
    /// instance of this class represents a source level basic type and the corresponding representation
    /// for LLVM.
    /// </remarks>
    /// <seealso href="xref:llvm_langref#dibasictype">LLVM DIBasicType</seealso>
    public class DebugBasicType
        : DebugType<ITypeRef, DIBasicType>
    {
        /// <summary>Initializes a new instance of the <see cref="DebugBasicType"/> class.</summary>
        /// <param name="llvmType">Type to wrap debug information for</param>
        /// <param name="diBuilder">Debug information builder for this module</param>
        /// <param name="name">Source language name of the type</param>
        /// <param name="encoding">Encoding for the type</param>
        public DebugBasicType( ITypeRef llvmType, IDIBuilder diBuilder, string name, DiTypeKind encoding )
            : base( llvmType,
                    diBuilder.CreateBasicType( name
                                             , diBuilder.OwningModule.Layout.BitSizeOf( llvmType )
                                             , encoding
                                             )
                  )
        {
            ArgumentException.ThrowIfNullOrWhiteSpace( name );

            if(diBuilder.OwningModule.Layout == null)
            {
                throw new ArgumentException( Resources.Module_needs_Layout_to_build_basic_types, $"{nameof( diBuilder )}.{nameof( diBuilder.OwningModule )}" );
            }

            switch(llvmType.Kind)
            {
            case TypeKind.Void:
            case TypeKind.Float16:
            case TypeKind.Float32:
            case TypeKind.Float64:
            case TypeKind.X86Float80:
            case TypeKind.Float128m112:
            case TypeKind.Float128:
            case TypeKind.Integer:
                break;

            default:
                throw new ArgumentException( Resources.Expected_a_primitive_type, nameof( llvmType ) );
            }
        }
    }
}
