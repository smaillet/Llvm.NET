﻿// -----------------------------------------------------------------------
// <copyright file="ModuleFlag.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Ubiquity.NET.Llvm.Metadata
{
    /// <summary>ModuleHandle Flags Tuple for a module</summary>
    public class ModuleFlag
    {
        /// <summary>Initializes a new instance of the <see cref="ModuleFlag"/> class.</summary>
        /// <param name="behavior">Behavior for the flag</param>
        /// <param name="name">Name of the flag</param>
        /// <param name="metadata">IrMetadata for the flag</param>
        public ModuleFlag( ModuleFlagBehavior behavior, LazyEncodedString name, IrMetadata metadata )
        {
            Behavior = behavior;
            Name = name;
            Metadata = metadata;
        }

        /// <summary>Gets the <see cref="ModuleFlagBehavior"/> options for this module flag</summary>
        public ModuleFlagBehavior Behavior { get; }

        /// <summary>Gets the name of flag</summary>
        public LazyEncodedString Name { get; }

        /// <summary>Gets the IrMetadata for this flag</summary>
        public IrMetadata Metadata { get; }

        internal ModuleFlag( MDNode node )
        {
            ArgumentNullException.ThrowIfNull( node );

            if(node.Operands.Count != 3)
            {
                throw new ArgumentException( Resources.Expected_node_with_3_operands, nameof( node ) );
            }

            if(node.Operands[ 0 ] is not ConstantAsMetadata behavior)
            {
                throw new ArgumentException( Resources.Expected_ConstantAsMetadata_for_first_operand, nameof( node ) );
            }

            if(behavior.Constant is not ConstantInt behaviorConst)
            {
                throw new ArgumentException( Resources.Expected_ConstantInt_wrapped_in_first_operand, nameof( node ) );
            }

            if(node.Operands[ 1 ] is not MDString nameMd)
            {
                throw new ArgumentException( Resources.Expected_MDString_as_second_operand, nameof( node ) );
            }

            Behavior = (ModuleFlagBehavior)(behaviorConst.ZeroExtendedValue);
            Name = nameMd.ToLazyEncodedString();
            Metadata = node.Operands[ 2 ]!;
        }
    }
}
