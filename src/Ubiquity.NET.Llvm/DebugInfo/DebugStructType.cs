﻿// -----------------------------------------------------------------------
// <copyright file="DebugStructType.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Ubiquity.NET.Llvm.DebugInfo
{
    /// <summary>Debug information for a structure type</summary>
    /// <seealso href="xref:llvm_langref#dicompositetype">LLVM DICompositeType</seealso>
    public class DebugStructType
        : DebugType<IStructType, DICompositeType>
        , IStructType
    {
        /// <summary>Initializes a new instance of the <see cref="DebugStructType"/> class.</summary>
        /// <param name="diBuilder">To construct the debug information for this instance</param>
        /// <param name="nativeName">Name of the type in LLVM IR (use <see cref="string.Empty"/> for anonymous types)</param>
        /// <param name="scope">Debug scope for the structure</param>
        /// <param name="sourceName">Source/debug name of the struct (use <see cref="string.Empty"/> for anonymous types)</param>
        /// <param name="file">File containing the definition of this type</param>
        /// <param name="line">line number this type is defined at</param>
        /// <param name="debugFlags">debug flags for this type</param>
        /// <param name="members">Description of all the members of this structure</param>
        /// <param name="derivedFrom">Base type, if any for this type</param>
        /// <param name="packed">Indicates if this type is packed or not</param>
        /// <param name="bitSize">Total bit size for this type or <see langword="null"/> to use default for target</param>
        /// <param name="bitAlignment">Alignment of the type in bits, 0 indicates default for target</param>
        public DebugStructType( IDIBuilder diBuilder
                              , string nativeName
                              , DIScope? scope
                              , string sourceName
                              , DIFile? file
                              , uint line
                              , DebugInfoFlags debugFlags
                              , IEnumerable<DebugMemberInfo> members
                              , DIType? derivedFrom = null
                              , bool packed = false
                              , uint? bitSize = null
                              , uint bitAlignment = 0
                              )
            : base( diBuilder.OwningModule.ThrowIfNull().Context.CreateStructType( nativeName, packed, [ .. members.Select( e => e.DebugType ) ] )
                  , diBuilder.CreateReplaceableCompositeType( Tag.StructureType
                                                            , sourceName
                                                            , scope
                                                            , file
                                                            , line
                                                            )
            )
        {
            DebugMembers = new ReadOnlyCollection<DebugMemberInfo>( members as IList<DebugMemberInfo> ?? [ .. members ] );

            // roll out the construction of elements as a builder is a ref struct and therefore
            // not allowed in a LINQ expression
            var memberTypes = new DINode[DebugMembers.Count];
            for(int i = 0; i < DebugMembers.Count; ++i)
            {
                memberTypes[ i ] = CreateMemberType( diBuilder, DebugMembers[ i ] );
            }

            var concreteType = diBuilder.CreateStructType( scope: scope
                                                         , name: sourceName
                                                         , file: file
                                                         , line: line
                                                         , bitSize: bitSize ?? diBuilder.OwningModule.Layout.BitSizeOf( NativeType )
                                                         , bitAlign: bitAlignment
                                                         , debugFlags: debugFlags
                                                         , derivedFrom: derivedFrom
                                                         , elements: memberTypes
                                                         );

            // assignment performs RAUW
            DebugInfoType = concreteType;
        }

        /// <summary>Initializes a new instance of the <see cref="DebugStructType"/> class.</summary>
        /// <param name="llvmType">LLVM native type to build debug information for</param>
        /// <param name="diBuilder">To construct the debug information for this instance</param>
        /// <param name="scope">Debug scope for the structure</param>
        /// <param name="name">Source/debug name of the struct (use <see cref="string.Empty"/> for anonymous types)</param>
        /// <param name="file">File containing the definition of this type</param>
        /// <param name="line">line number this type is defined at</param>
        /// <param name="debugFlags">debug flags for this type</param>
        /// <param name="elements">Debug type of all the members of this structure</param>
        /// <param name="derivedFrom">Base type, if any for this type</param>
        /// <param name="bitAlignment">Alignment of the type in bits, 0 indicates default for target</param>
        public DebugStructType( IStructType llvmType
                              , IDIBuilder diBuilder
                              , DIScope? scope
                              , string name
                              , DIFile? file
                              , uint line
                              , DebugInfoFlags debugFlags
                              , DIType? derivedFrom
                              , IEnumerable<DIType> elements
                              , uint bitAlignment = 0
                              )
            : base( llvmType
                  , diBuilder.CreateStructType( scope
                                              , name
                                              , file
                                              , line
                                              , diBuilder.OwningModule.Layout.BitSizeOf( llvmType )
                                              , bitAlignment
                                              , debugFlags
                                              , derivedFrom
                                              , elements
                                              )
                  )
        {
        }

        /// <summary>Initializes a new instance of the <see cref="DebugStructType"/> class.</summary>
        /// <param name="llvmType">LLVM native type to build debug information for</param>
        /// <param name="diBuilder">To construct the debug information for this instance</param>
        /// <param name="scope">Debug scope for the structure</param>
        /// <param name="name">Source/debug name of the struct (use <see cref="string.Empty"/> for anonymous types)</param>
        /// <param name="file">File containing the definition of this type</param>
        /// <param name="line">line number this type is defined at</param>
        /// <remarks>
        /// This constructor creates a replaceable type that is replaced later with a full
        /// definition of the type
        /// </remarks>
        public DebugStructType( IStructType llvmType
                              , IDIBuilder diBuilder
                              , DIScope? scope
                              , string name
                              , DIFile? file
                              , uint line
                              )
            : base( llvmType
                  , diBuilder.CreateReplaceableCompositeType( Tag.StructureType
                                                            , name
                                                            , scope
                                                            , file
                                                            , line
                                                            )
                  )
        {
        }

        /// <summary>Initializes a new instance of the <see cref="DebugStructType"/> class.</summary>
        /// <param name="diBuilder">To construct the debug information for this instance</param>
        /// <param name="nativeName">Name of the type in LLVM IR</param>
        /// <param name="scope">Debug scope for the structure</param>
        /// <param name="name">Source/debug name of the struct (use <see cref="string.Empty"/> for anonymous types)</param>
        /// <param name="file">File containing the definition of this type</param>
        /// <param name="line">line number this type is defined at</param>
        /// <remarks>
        /// This constructor creates a replaceable type that is replaced later with a full
        /// definition of the type
        /// </remarks>
        public DebugStructType( IDIBuilder diBuilder
                              , string nativeName
                              , DIScope? scope
                              , string name
                              , DIFile? file = null
                              , uint line = 0
                              )
            : this( diBuilder.OwningModule.Context.CreateStructType( nativeName )
                  , diBuilder
                  , scope
                  , name
                  , file
                  , line
                  )
        {
        }

        /// <summary>Gets a value indicating whether the type is Opaque (e.g. has no body)</summary>
        public bool IsOpaque => NativeType.IsOpaque;

        /// <inheritdoc/>
        public bool IsPacked => NativeType.IsPacked;

        /// <summary>Gets the members of the type</summary>
        public IReadOnlyList<ITypeRef> Members => NativeType.Members;

        /// <summary>Gets the name of the type</summary>
        public string Name => NativeType.Name ?? string.Empty;

        /// <summary>Gets the Source/Debug name</summary>
        public string SourceName => DebugInfoType?.Name ?? string.Empty;

        /// <inheritdoc/>
        public void SetBody( bool packed, params IEnumerable<ITypeRef> elements )
        {
            NativeType.SetBody( packed, elements );
        }

        /// <summary>Set the body of a type</summary>
        /// <param name="packed">Flag to indicate if the body elements are packed (e.g. no padding)</param>
        /// <param name="diBuilder">To construct the debug information for this instance</param>
        /// <param name="scope">Scope containing this type</param>
        /// <param name="file">File containing the type</param>
        /// <param name="line">Line in <paramref name="file"/> for this type</param>
        /// <param name="debugFlags">Debug flags for this type</param>
        /// <param name="debugElements">Descriptors for all the elements in the type</param>
        public void SetBody( bool packed
                           , IDIBuilder diBuilder
                           , DIScope? scope
                           , DIFile? file
                           , uint line
                           , DebugInfoFlags debugFlags
                           , params IEnumerable<DebugMemberInfo> debugElements
                           )
        {
            var debugMembersArray = debugElements as IList<DebugMemberInfo> ?? [ .. debugElements ];
            var nativeElements = debugMembersArray.Select( e => e.DebugType.NativeType );
            SetBody( packed, diBuilder, scope, file, line, debugFlags, nativeElements, debugMembersArray );
        }

        /// <summary>Set the body of a type</summary>
        /// <param name="packed">Flag to indicate if the body elements are packed (e.g. no padding)</param>
        /// <param name="diBuilder">Debug information builder to use for generating debug information</param>
        /// <param name="scope">Scope containing this type</param>
        /// <param name="file">File containing the type</param>
        /// <param name="line">Line in <paramref name="file"/> for this type</param>
        /// <param name="debugFlags">Debug flags for this type</param>
        /// <param name="nativeElements">LLVM type of each element</param>
        /// <param name="debugElements">Descriptors for each element in the type</param>
        /// <param name="derivedFrom">Base type, if any for this type</param>
        /// <param name="bitSize">Total bit size for this type or <see langword="null"/> to use default for target</param>
        /// <param name="bitAlignment">Alignment of the type in bits, 0 indicates default for target</param>
        public void SetBody( bool packed
                           , IDIBuilder diBuilder
                           , DIScope? scope
                           , DIFile? file
                           , uint line
                           , DebugInfoFlags debugFlags
                           , IEnumerable<ITypeRef> nativeElements
                           , IEnumerable<DebugMemberInfo> debugElements
                           , DIType? derivedFrom = null
                           , uint? bitSize = null
                           , uint bitAlignment = 0
                           )
        {
            ArgumentNullException.ThrowIfNull( nativeElements );
            ArgumentNullException.ThrowIfNull( debugElements );

            DebugMembers = new ReadOnlyCollection<DebugMemberInfo>( debugElements as IList<DebugMemberInfo> ?? [ .. debugElements ] );
            SetBody( packed, [ .. nativeElements ] );

            var memberTypes = new DINode[DebugMembers.Count];
            for(int i = 0; i < DebugMembers.Count; ++i)
            {
                memberTypes[ i ] = CreateMemberType( diBuilder, DebugMembers[ i ] );
            }

            var concreteType = diBuilder.CreateStructType( scope: scope
                                                         , name: DebugInfoType?.Name ?? string.Empty
                                                         , file: file
                                                         , line: line
                                                         , bitSize: bitSize ?? diBuilder.OwningModule.Layout.BitSizeOf( NativeType )
                                                         , bitAlign: bitAlignment
                                                         , debugFlags: debugFlags
                                                         , derivedFrom: derivedFrom
                                                         , elements: memberTypes
                                                         );
            DebugInfoType = concreteType;
        }

        /// <summary>Gets a list of descriptors for each members</summary>
        public IReadOnlyList<DebugMemberInfo> DebugMembers { get; private set; } = new List<DebugMemberInfo>().AsReadOnly();

        private DIDerivedType CreateMemberType( IDIBuilder diBuilder, DebugMemberInfo memberInfo )
        {
            if(DebugInfoType == null)
            {
                throw new InvalidOperationException( Resources.Type_does_not_have_associated_Debug_type_from_which_to_construct_a_Member );
            }

            UInt64 bitSize;
            UInt32 bitAlign;
            UInt64 bitOffset;

            // if explicit layout info provided, use it;
            // otherwise use module.Layout as the default
            if(memberInfo.ExplicitLayout != null)
            {
                bitSize = memberInfo.ExplicitLayout.BitSize;
                bitAlign = memberInfo.ExplicitLayout.BitAlignment;
                bitOffset = memberInfo.ExplicitLayout.BitOffset;
            }
            else
            {
                bitSize = diBuilder.OwningModule.Layout.BitSizeOf( memberInfo.DebugType.NativeType );
                bitAlign = 0;
                bitOffset = diBuilder.OwningModule.Layout.BitOffsetOfElement( NativeType, memberInfo.Index );
            }

            return diBuilder.CreateMemberType( scope: DebugInfoType
                                             , name: memberInfo.Name
                                             , file: memberInfo.File
                                             , line: memberInfo.Line
                                             , bitSize: bitSize
                                             , bitAlign: bitAlign
                                             , bitOffset: bitOffset
                                             , debugFlags: memberInfo.DebugInfoFlags
                                             , type: memberInfo.DebugType.DebugInfoType
                                             );
        }
    }
}
