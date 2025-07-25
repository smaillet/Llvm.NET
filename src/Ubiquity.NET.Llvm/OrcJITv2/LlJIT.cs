﻿// -----------------------------------------------------------------------
// <copyright file="LlJIT.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using static Ubiquity.NET.Llvm.Interop.ABI.llvm_c.LLJIT;
using static Ubiquity.NET.Llvm.Interop.ABI.llvm_c.LLJITUtils;

// They are order by access, unfortunately this analyzer has stupid built-in defaults that
// puts internal as higher priority than protected and no way to override it.
#pragma warning disable SA1202 // Elements should be ordered by access

namespace Ubiquity.NET.Llvm.OrcJITv2
{
    /// <summary>ORC v2 LLJIT instance</summary>
    public sealed class LlJIT
        : IDisposable
        , IOrcJit
    {
        /// <summary>Initializes a new instance of the <see cref="LlJIT"/> class.</summary>
        [SuppressMessage( "Reliability", "CA2000:Dispose objects before losing scope", Justification = "Ownership is held in this instance" )]
        public LlJIT( )
            : this( CreateDefaultWithoutBuilder() )
        {
        }

        /// <summary>Gets the main library for this JIT instance</summary>
        public JITDyLib MainLib => new( LLVMOrcLLJITGetMainJITDylib( Handle ) );

        /// <summary>Gets the global prefix character for this instance</summary>
        public char GlobalPrefix => (char)LLVMOrcLLJITGetGlobalPrefix( Handle );

        /// <summary>Gets the data layout string for this JIT</summary>
        public LazyEncodedString DataLayoutString => LLVMOrcLLJITGetDataLayoutStr( Handle );

        /*
        TODO: Add LibLLVMxxx to make this go away, the underlying JIT HAS a Triple instance!
              So this is building a string from that, then passed around as a string marshaled to/from
              the native abi and then re-parsed back to a !@#$ Triple again - WASTED overhead!
        */

        /// <summary>Gets a string representation of the target triple for this JIT</summary>
        public LazyEncodedString TripleString => LLVMOrcLLJITGetTripleString( Handle );

        /// <summary>Adds a module to this JIT with removal tracking</summary>
        /// <param name="ctx">Thread safe context this module is part of</param>
        /// <param name="module">Module to add</param>
        /// <param name="lib">Library to work on</param>
        /// <returns>Resource tracker for this instance</returns>
        [MustUseReturnValue]
        public ResourceTracker AddWithTracking( ThreadSafeContext ctx, Module module, JITDyLib lib = default )
        {
            ArgumentNullException.ThrowIfNull( ctx );
            ArgumentNullException.ThrowIfNull( module );

            // Default to using MainLib if none specified.
            if(lib.Handle.IsNull)
            {
                lib = MainLib;
            }

            ResourceTracker tracker = lib.CreateResourceTracker();
            try
            {
                using ThreadSafeModule tsm = new(ctx, module);
                Add( tracker, tsm );
            }
            catch
            {
                tracker.Dispose();
                throw;
            }

            return tracker;
        }

        /// <summary>Looks up the native address of a symbol</summary>
        /// <param name="name">NameField of the symbol to find the address of</param>
        /// <returns>Address of the symbol</returns>
        /// <exception cref="LlvmException">Error occurred with lookup [Most likely the symbol is not found]</exception>
        public UInt64 Lookup( LazyEncodedString name )
        {
            ArgumentNullException.ThrowIfNull( name );

            // Deal with bad API design, converting errors to an exception and returning the result.
            using LLVMErrorRef errorRef = LLVMOrcLLJITLookup(Handle, out UInt64 retVal, name);
            errorRef.ThrowIfFailed();
            return retVal;
        }

        /// <summary>Adds a module to the JIT</summary>
        /// <param name="lib">Library to add the module to in this JIT</param>
        /// <param name="module">Module to add</param>
        /// <remarks>
        /// This function has "move" semantics in that the JIT takes ownership of the
        /// input module and it is no longer usable (Generates <see cref="ObjectDisposedException"/>)
        /// for any use other than Dispose(). This allows normal clean up in the event of an exception
        /// to occur.
        /// <note type="important">
        /// Transfer of ownership does NOT occur in the face of an error (exception)! However the
        /// <see cref="ThreadSafeModule.Dispose"/> method is idempotent and will NOT throw an exception
        /// if disposed so it is safe to declare instances with a "using".
        /// </note>
        /// </remarks>
        public void Add( JITDyLib lib, ThreadSafeModule module )
        {
            ArgumentNullException.ThrowIfNull( module );

            using LLVMErrorRef errRef = LLVMOrcLLJITAddLLVMIRModule(Handle, lib.Handle, module.Handle);
            errRef.ThrowIfFailed();
            module.Handle.SetHandleAsInvalid(); // transfer to native complete, handle is no longer usable
        }

        /// <summary>Adds a module to the JIT</summary>
        /// <param name="tracker">Resource tracker to manage the module</param>
        /// <param name="module">Module to add</param>
        /// <remarks>
        /// This function has "move" semantics in that the JIT takes ownership of the
        /// input module and it is no longer usable (Generates <see cref="ObjectDisposedException"/>)
        /// for any use other than Dispose(). This allows normal clean up in the event of an exception
        /// to occur.
        /// <note type="important">
        /// Transfer of ownership does NOT occur in the face of an error (exception)! However the
        /// <see cref="ThreadSafeModule.Dispose"/> method is idempotent and will NOT throw an exception
        /// if disposed so it is safe to declare instances with a "using".
        /// </note>
        /// </remarks>
        public void Add( ResourceTracker tracker, ThreadSafeModule module )
        {
            ArgumentNullException.ThrowIfNull( tracker );
            ArgumentNullException.ThrowIfNull( module );

            using LLVMErrorRef errorRef = LLVMOrcLLJITAddLLVMIRModuleWithRT(Handle, tracker.Handle, module.Handle);
            errorRef.ThrowIfFailed();
            module.Handle.SetHandleAsInvalid(); // transfer to native complete, handle is no longer usable
        }

        /// <summary>Mangles and interns a symbol in the JIT's symbol pool</summary>
        /// <param name="name">Symbol name to add</param>
        /// <returns>Entry to the string pool for the symbol</returns>
        public SymbolStringPoolEntry MangleAndIntern( LazyEncodedString name )
        {
            ArgumentNullException.ThrowIfNull( name );
            return new( LLVMOrcLLJITMangleAndIntern( Handle, name ) );
        }

        /// <summary>Gets the IR transform layer for this JIT</summary>
        public IrTransformLayer TransformLayer => new( LLVMOrcLLJITGetIRTransformLayer( Handle ) );

        /// <summary>Gets the Execution session for this JIT</summary>
        public ExecutionSession Session => new( LLVMOrcLLJITGetExecutionSession( Handle ) );

        /// <summary>Enables debug support in the JIT</summary>
        /// <remarks>
        /// This installs the plug-in that submits debug objects to the executor. Executors must
        /// expose the llvm_orc_registerJITLoaderGDBWrapper symbol.
        /// </remarks>
        public void EnableDebugSupport( )
        {
            using var err = LLVMOrcLLJITEnableDebugSupport( Handle );
            err.ThrowIfFailed();
        }

        /// <inheritdoc/>
        public void Dispose( )
        {
            Handle.Dispose();
        }

        internal LlJIT( LLVMOrcLLJITRef h )
        {
            Handle = h.Move();
        }

        private readonly LLVMOrcLLJITRef Handle;

        private static LLVMOrcLLJITRef CreateDefaultWithoutBuilder( )
        {
            using var errorRef = LLVMOrcCreateLLJIT(LLVMOrcLLJITBuilderRef.Zero, out LLVMOrcLLJITRef retVal);
            errorRef.ThrowIfFailed();
            return retVal;
        }
    }
}
