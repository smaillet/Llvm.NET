// -----------------------------------------------------------------------
// <copyright file="LocalAsMetadata.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Ubiquity.NET.Llvm.Metadata
{
    /// <summary>Contains a local Value as IrMetadata</summary>
    public class LocalAsMetadata
        : ValueAsMetadata
    {
        internal LocalAsMetadata( LLVMMetadataRef handle )
            : base( handle )
        {
        }

        /*
        static public LocalAsMetadata GetIfExists(Value local);
        static public LocalAsMetadata Create(Value local);
        */
    }
}
