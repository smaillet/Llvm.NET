﻿// -----------------------------------------------------------------------
// <copyright file="BasicBlock.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using static Ubiquity.NET.Llvm.Interop.ABI.llvm_c.Core;

namespace Ubiquity.NET.Llvm.Values
{
    /// <summary>Provides access to an LLVM Basic block</summary>
    /// <remarks>
    /// A basic block is a sequence of instructions with a single entry
    /// and a single exit. The exit point must be a <see cref="Terminator"/>
    /// instruction or the block is not (yet) well-formed.
    /// </remarks>
    public class BasicBlock
        : Value
    {
        /// <summary>Gets the function containing the block</summary>
        public Function? ContainingFunction
        {
            get
            {
                var parent = LLVMGetBasicBlockParent( BlockHandle );
                if(parent == default)
                {
                    return null;
                }

                // cache functions and use lookups to ensure
                // identity/interning remains consistent with actual
                // LLVM model of interning
                return FromHandle<Function>( parent );
            }
        }

        /// <summary>Gets the first instruction in the block</summary>
        public Instruction? FirstInstruction
        {
            get
            {
                var firstInst = LLVMGetFirstInstruction( BlockHandle );
                return firstInst == default ? null : FromHandle<Instruction>( firstInst );
            }
        }

        /// <summary>Gets the last instruction in the block</summary>
        public Instruction? LastInstruction
        {
            get
            {
                var lastInst = LLVMGetLastInstruction( BlockHandle );
                return lastInst == default ? null : FromHandle<Instruction>( lastInst );
            }
        }

        /// <summary>Gets the terminator instruction for the block</summary>
        /// <remarks>
        /// May be null if the block is not yet well-formed
        /// as is commonly the case while generating code for a new block
        /// </remarks>
        public Instruction? Terminator
        {
            get
            {
                var terminator = LLVMGetBasicBlockTerminator( BlockHandle );
                return terminator == default ? null : FromHandle<Instruction>( terminator );
            }
        }

        /// <summary>Gets all instructions in the block</summary>
        public IEnumerable<Instruction> Instructions
        {
            get
            {
                var current = FirstInstruction;
                while(current != null)
                {
                    yield return current;
                    current = GetNextInstruction( current );
                }
            }
        }

        /// <summary>Gets the instruction that follows a given instruction in a block</summary>
        /// <param name="instruction">instruction in the block to get the next instruction from</param>
        /// <returns>Next instruction or null if none</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref cref="Instruction"/> is from a different block</exception>
        public Instruction? GetNextInstruction( Instruction instruction )
        {
            ArgumentNullException.ThrowIfNull( instruction );

            if(!instruction.ContainingBlock.Equals( this ))
            {
                throw new ArgumentException( Resources.Instruction_is_from_a_different_block, nameof( instruction ) );
            }

            var hInst = LLVMGetNextInstruction( instruction.Handle );
            return hInst == default ? null : FromHandle<Instruction>( hInst );
        }

        internal BasicBlock( LLVMValueRef valueRef )
            : base( valueRef )
        {
        }

        internal LLVMBasicBlockRef BlockHandle => LLVMValueAsBasicBlock( Handle );

        internal static BasicBlock? FromHandle( LLVMBasicBlockRef basicBlockRef )
        {
            return FromHandle<BasicBlock>( LLVMBasicBlockAsValue( basicBlockRef ) );
        }
    }
}
