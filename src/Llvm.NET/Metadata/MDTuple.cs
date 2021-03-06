// -----------------------------------------------------------------------
// <copyright file="MDTuple.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Llvm.NET.Interop;

namespace Llvm.NET
{
    /// <summary>Tuple of Metadata nodes</summary>
    /// <remarks>
    /// This acts as a container of nodes in the metadata hierarchy
    /// </remarks>
    public class MDTuple : MDNode
    {
        internal MDTuple( LLVMMetadataRef handle )
            : base( handle )
        {
        }
    }
}
