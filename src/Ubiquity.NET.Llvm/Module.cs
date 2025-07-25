﻿// -----------------------------------------------------------------------
// <copyright file="Module.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using static Ubiquity.NET.Llvm.Interop.ABI.llvm_c.BitReader;

namespace Ubiquity.NET.Llvm
{
    /// <summary>Enumeration to indicate the behavior of module level flags metadata sharing the same name in a <see cref="Module"/></summary>
    [SuppressMessage( "Design", "CA1027:Mark enums with FlagsAttribute", Justification = "It isn't a flags enum" )]
    public enum ModuleFlagBehavior
    {
        /// <summary>Emits an error if two values disagree, otherwise the resulting value is that of the operands</summary>
        Error = LLVMModuleFlagBehavior.LLVMModuleFlagBehaviorError,

        /// <summary>Emits a warning if two values disagree. The result will be the operand for the flag from the first module being linked</summary>
        Warning = LLVMModuleFlagBehavior.LLVMModuleFlagBehaviorWarning,

        /// <summary>Adds a requirement that another module flag be present and have a specified value after linking is performed</summary>
        /// <remarks>
        /// The value must be a metadata pair, where the first element of the pair is the ID of the module flag to be restricted, and the
        /// second element of the pair is the value the module flag should be restricted to. This behavior can be used to restrict the
        /// allowable results (via triggering of an error) of linking IDs with the <see cref="Override"/> behavior
        /// </remarks>
        Require = LLVMModuleFlagBehavior.LLVMModuleFlagBehaviorRequire,

        /// <summary>Uses the specified value, regardless of the behavior or value of the other module</summary>
        /// <remarks>If both modules specify Override, but the values differ, and error will be emitted</remarks>
        Override = LLVMModuleFlagBehavior.LLVMModuleFlagBehaviorOverride,

        /// <summary>Appends the two values, which are required to be metadata nodes</summary>
        Append = LLVMModuleFlagBehavior.LLVMModuleFlagBehaviorAppend,

        /// <summary>Appends the two values, which are required to be metadata nodes dropping duplicate entries in the second list</summary>
        AppendUnique = LLVMModuleFlagBehavior.LLVMModuleFlagBehaviorAppendUnique,
    }

    /// <summary>LLVM Bitcode module</summary>
    /// <remarks>
    /// A module is the basic unit for containing code in LLVM. Modules are an in memory
    /// representation of the LLVM Intermediate Representation (IR) bit-code.
    /// </remarks>
    public sealed class Module
        : IModule
        , IGlobalHandleOwner<LLVMModuleRef>
        , IDisposable
        , IEquatable<Module>
    {
        #region IEquatable<>

        /// <inheritdoc/>
        public bool Equals( IModule? other ) => other is not null && ((LLVMModuleRefAlias)NativeHandle).Equals( other.GetUnownedHandle() );

        /// <inheritdoc/>
        public bool Equals( Module? other ) => other is not null && NativeHandle.Equals( other.NativeHandle );

        /// <inheritdoc/>
        public override bool Equals( object? obj ) => obj is Module owner
                                                  ? Equals( owner )
                                                  : Equals( obj as IModule );

        /// <inheritdoc/>
        public override int GetHashCode( ) => NativeHandle.GetHashCode();
        #endregion

        /// <summary>Name of the Debug Version information module flag</summary>
        public static readonly LazyEncodedString DebugVersionValue = "Debug Info Version"u8;

        /// <summary>Name of the Dwarf Version module flag</summary>
        public static readonly LazyEncodedString DwarfVersionValue = "Dwarf Version"u8;

        /// <summary>Gets a value indicating whether this instance is already disposed</summary>
        public bool IsDisposed => NativeHandle is null || NativeHandle.IsInvalid || NativeHandle.IsClosed;

        /// <inheritdoc/>
        public void Dispose( ) => NativeHandle.Dispose();

        #region IModule (via Impl)

        /// <inheritdoc/>
        public void AppendInlineAsm( LazyEncodedString asm ) => Impl.AppendInlineAsm( asm );

        /// <inheritdoc/>
        public ErrorInfo TryRunPasses( params LazyEncodedString[] passes ) => Impl.TryRunPasses( passes );

        /// <inheritdoc/>
        public ErrorInfo TryRunPasses( PassBuilderOptions options, params LazyEncodedString[] passes ) => Impl.TryRunPasses( options, passes );

        /// <inheritdoc/>
        public ErrorInfo TryRunPasses( TargetMachine targetMachine, PassBuilderOptions options, params LazyEncodedString[] passes ) => Impl.TryRunPasses( targetMachine, options, passes );

        /// <inheritdoc/>
        public void Link( Module srcModule ) => Impl.Link( srcModule );

        /// <inheritdoc/>
        public bool Verify( out string errorMessage ) => Impl.Verify( out errorMessage );

        /// <inheritdoc/>
        public bool TryGetFunction( LazyEncodedString name, [MaybeNullWhen( false )] out Function function ) => Impl.TryGetFunction( name, out function );

        /// <inheritdoc/>
        public GlobalIFunc CreateAndAddGlobalIFunc( LazyEncodedString name, ITypeRef type, uint addressSpace, Function resolver ) => Impl.CreateAndAddGlobalIFunc( name, type, addressSpace, resolver );

        /// <inheritdoc/>
        public bool TryGetNamedGlobalIFunc( LazyEncodedString name, [MaybeNullWhen( false )] out GlobalIFunc function ) => Impl.TryGetNamedGlobalIFunc( name, out function );

        /// <inheritdoc/>
        public Function CreateFunction( LazyEncodedString name, IFunctionType signature ) => Impl.CreateFunction( name, signature );

        /// <inheritdoc/>
        public Function CreateFunction( IDIBuilder diBuilder
                                      , DIScope? scope
                                      , LazyEncodedString name
                                      , LazyEncodedString? linkageName
                                      , DIFile? file
                                      , uint line
                                      , DebugFunctionType signature
                                      , bool isLocalToUnit
                                      , bool isDefinition
                                      , uint scopeLine
                                      , DebugInfoFlags debugFlags
                                      , bool isOptimized
                                      )
         {
            return Impl.CreateFunction( diBuilder
                                      , scope
                                      , name
                                      , linkageName
                                      , file
                                      , line
                                      , signature
                                      , isLocalToUnit
                                      , isDefinition
                                      , scopeLine
                                      , debugFlags
                                      , isOptimized
                                      );
         }

        /// <inheritdoc/>
        public Function CreateFunction( IDIBuilder diBuilder
                                      , LazyEncodedString name
                                      , bool isVarArg
                                      , IDebugType<ITypeRef, DIType> returnType
                                      , params IEnumerable<IDebugType<ITypeRef, DIType>> argumentTypes
                                      )
        {
            return Impl.CreateFunction( diBuilder, name, isVarArg, returnType, argumentTypes );
        }

        /// <inheritdoc/>
        public void WriteToFile( string path ) => Impl.WriteToFile( path );

        /// <inheritdoc/>
        public bool WriteToTextFile( string path, out string errMsg ) => Impl.WriteToTextFile( path, out errMsg );

        /// <inheritdoc/>
        public string? WriteToString( ) => Impl.WriteToString();

        /// <inheritdoc/>
        public MemoryBuffer WriteToBuffer( ) => Impl.WriteToBuffer();

        /// <inheritdoc/>
        public GlobalAlias AddAlias( Value aliasee, LazyEncodedString aliasName, uint addressSpace = 0 ) => Impl.AddAlias( aliasee, aliasName, addressSpace );

        /// <inheritdoc/>
        public GlobalAlias? GetAlias( LazyEncodedString name ) => Impl.GetAlias( name );

        /// <inheritdoc/>
        public GlobalVariable AddGlobalInAddressSpace( uint addressSpace, ITypeRef typeRef, LazyEncodedString name ) => Impl.AddGlobalInAddressSpace( addressSpace, typeRef, name );

        /// <inheritdoc/>
        public GlobalVariable AddGlobalInAddressSpace( uint addressSpace, ITypeRef typeRef, bool isConst, Linkage linkage, Constant constVal ) => Impl.AddGlobalInAddressSpace( addressSpace, typeRef, isConst, linkage, constVal );

        /// <inheritdoc/>
        public GlobalVariable AddGlobalInAddressSpace( uint addressSpace, ITypeRef typeRef, bool isConst, Linkage linkage, Constant constVal, LazyEncodedString name ) => Impl.AddGlobalInAddressSpace( addressSpace, typeRef, isConst, linkage, constVal, name );

        /// <inheritdoc/>
        public GlobalVariable AddGlobal( ITypeRef typeRef, LazyEncodedString name ) => Impl.AddGlobal( typeRef, name );

        /// <inheritdoc/>
        public GlobalVariable AddGlobal( ITypeRef typeRef, bool isConst, Linkage linkage, Constant constVal ) => Impl.AddGlobal( typeRef, isConst, linkage, constVal );

        /// <inheritdoc/>
        public GlobalVariable AddGlobal( ITypeRef typeRef, bool isConst, Linkage linkage, Constant constVal, LazyEncodedString name ) => Impl.AddGlobal( typeRef, isConst, linkage, constVal, name );

        /// <inheritdoc/>
        public ITypeRef? GetTypeByName( LazyEncodedString name ) => Impl.GetTypeByName( name );

        /// <inheritdoc/>
        public GlobalVariable? GetNamedGlobal( LazyEncodedString name ) => Impl.GetNamedGlobal( name );

        /// <inheritdoc/>
        public void AddModuleFlag( ModuleFlagBehavior behavior, LazyEncodedString name, uint value ) => Impl.AddModuleFlag( behavior, name, value );

        /// <inheritdoc/>
        public void AddModuleFlag( ModuleFlagBehavior behavior, LazyEncodedString name, IrMetadata value ) => Impl.AddModuleFlag( behavior, name, value );

        /// <inheritdoc/>
        public void AddNamedMetadataOperand( LazyEncodedString name, IrMetadata value ) => Impl.AddNamedMetadataOperand( name, value );

        /// <inheritdoc/>
        public void AddVersionIdentMetadata( LazyEncodedString version ) => Impl.AddVersionIdentMetadata( version );

        /// <inheritdoc/>
        public Function GetIntrinsicDeclaration( LazyEncodedString name, params ITypeRef[] args ) => Impl.GetIntrinsicDeclaration( name, args );

        /// <inheritdoc/>
        public Function GetIntrinsicDeclaration( uint id, params ITypeRef[] args ) => Impl.GetIntrinsicDeclaration( id, args );

        /// <inheritdoc/>
        public Module Clone( ) => Impl.Clone();

        /// <inheritdoc/>
        public Module Clone( IContext targetContext ) => Impl.Clone( targetContext );

        /// <inheritdoc/>
        public bool StripDebugInformation() => Impl.StripDebugInformation();

        /// <inheritdoc/>
        public LazyEncodedString SourceFileName { get => Impl.SourceFileName; set => Impl.SourceFileName = value; }

        /// <inheritdoc/>
        public ComdatCollection Comdats => Impl.Comdats;

        /// <inheritdoc/>
        public IContext Context => Impl.Context;

        /// <inheritdoc/>
        public IReadOnlyDictionary<LazyEncodedString, ModuleFlag> ModuleFlags => Impl.ModuleFlags;

        /// <inheritdoc/>
        public IEnumerable<DICompileUnit> CompileUnits => Impl.CompileUnits;

        /// <inheritdoc/>
        [DisallowNull]
        public LazyEncodedString DataLayoutString { get => Impl.DataLayoutString; set => Impl.DataLayoutString = value; }

        /// <inheritdoc/>
        public IDataLayout Layout { get => Impl.Layout; set => Impl.Layout = value; }

        /// <inheritdoc/>
        public LazyEncodedString TargetTriple { get => Impl.TargetTriple; set => Impl.TargetTriple = value; }

        /// <inheritdoc/>
        public IEnumerable<GlobalVariable> Globals => Impl.Globals;

        /// <inheritdoc/>
        public IEnumerable<Function> Functions => Impl.Functions;

        /// <inheritdoc/>
        public IEnumerable<GlobalAlias> Aliases => Impl.Aliases;

        /// <inheritdoc/>
        public IEnumerable<NamedMDNode> NamedMetadata => Impl.NamedMetadata;

        /// <inheritdoc/>
        public IEnumerable<GlobalIFunc> IndirectFunctions => Impl.IndirectFunctions;

        /// <inheritdoc/>
        public LazyEncodedString Name => Impl.Name;

        /// <inheritdoc/>
        public LazyEncodedString ModuleInlineAsm { get => Impl.ModuleInlineAsm; set => Impl.ModuleInlineAsm = value; }

        /// <inheritdoc/>
        public uint DebugMetadataVersion => Impl.DebugMetadataVersion;
        #endregion

        /// <summary>Load a bit-code module from a given file</summary>
        /// <param name="path">path of the file to load</param>
        /// <param name="context">Context to use for creating the module</param>
        /// <returns>Loaded <see cref="Module"/></returns>
        public static Module LoadFrom( LazyEncodedString path, IContext context )
        {
            ArgumentException.ThrowIfNullOrWhiteSpace( path );
            ArgumentNullException.ThrowIfNull( context );

            if(!File.Exists( path ))
            {
                throw new FileNotFoundException( Resources.Specified_bit_code_file_does_not_exist, path );
            }

            using var buffer = new MemoryBuffer( path );
            return LoadFrom( buffer, context );
        }

        /// <summary>Load bit code from a memory buffer</summary>
        /// <param name="buffer">Buffer to load from</param>
        /// <param name="context">Context to load the module into</param>
        /// <returns>Loaded <see cref="Module"/></returns>
        /// <remarks>
        /// This along with <see cref="WriteToBuffer"/> are useful for "cloning"
        /// a module from one context to another. This allows creation of multiple
        /// modules on different threads and contexts and later moving them to a
        /// single context in order to link them into a single final module for
        /// optimization.
        /// </remarks>
        public static Module LoadFrom( MemoryBuffer buffer, IContext context )
        {
            ArgumentNullException.ThrowIfNull( buffer );
            ArgumentNullException.ThrowIfNull( context );

#pragma warning disable CA2000 // Dispose objects before losing scope

            // modRef is invalid on failed conditions and transferred to new module on success; Dispose not needed
            return LLVMParseBitcodeInContext2( context.GetUnownedHandle(), buffer.Handle, out LLVMModuleRef modRef ).Failed
                ? throw new InternalCodeGeneratorException( Resources.Could_not_parse_bit_code_from_buffer )
                : new( modRef );
#pragma warning restore CA2000 // Dispose objects before losing scope
        }

        internal Module( LLVMModuleRef h )
        {
            NativeHandle = h.Move();
            AliasImpl = new( NativeHandle );
        }

        /// <inheritdoc/>
        [SuppressMessage( "StyleCop.CSharp.OrderingRules", "SA1202:Elements should be ordered by access", Justification = "internal interface" )]
        LLVMModuleRef IGlobalHandleOwner<LLVMModuleRef>.OwnedHandle => NativeHandle;

        /// <inheritdoc/>
        void IGlobalHandleOwner<LLVMModuleRef>.InvalidateFromMove( ) => NativeHandle.SetHandleAsInvalid();

        private ModuleAlias Impl
        {
            get
            {
                ObjectDisposedException.ThrowIf( IsDisposed, this );
                return AliasImpl;
            }
        }

        private readonly ModuleAlias AliasImpl;
        private readonly LLVMModuleRef NativeHandle;
    }
}
