// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 2.17941.31104.49410
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace Llvm.NET.Interop
{
    /// <summary>Simple typesafe handle to wrap a raw pointer for interop with "C" API exported from LibLLVM</summary>
    /// <remarks>
    ///    This handle is owned by it's container and therefore isn't disposed by the
    ///    calling App.
    /// <note type="important">
    ///     Since this is not owned by the App, the item it references is destroyed,
    ///     whenever it's container is destroyed, which will invalidate this handle.
    ///     use of this handle after the container is destroyed will produce undefined
    ///     behavior, includingly, and most likely, memory access violations.
    /// </note>
    /// </remarks>
    [GeneratedCode("LlvmBindingsGenerator","2.17941.31104.49410")]
    public struct LLVMErrorTypeId
        : IEquatable<LLVMErrorTypeId>
    {
        /// <inheritdoc/>
        public override int GetHashCode( ) => Handle.GetHashCode( );

        /// <inheritdoc/>
        public override bool Equals( object obj )
            => !( obj is null )
             && ( obj is LLVMErrorTypeId r )
             && ( r.Handle == Handle );

        /// <summary>Tests another for reference equality</summary>
        /// <param name="other">Other block to compare</param>
        /// <returns><see langword="true"/> if the other handle refers to the same native object (e.g. reference equality)</returns>
        public bool Equals( LLVMErrorTypeId other ) => Handle == other.Handle;

        /// <summary>Tests two handles for reference equality</summary>
        /// <param name="lhs">Left side of comparison</param>
        /// <param name="rhs">Right side of comparison</param>
        /// <returns><see langword="true"/> if both handles refer to the same native object (e.g. reference equality)</returns>
        public static bool operator ==( LLVMErrorTypeId lhs, LLVMErrorTypeId rhs )
            => EqualityComparer<LLVMErrorTypeId>.Default.Equals( lhs, rhs );

        /// <summary>Tests two handles for reference inequality</summary>
        /// <param name="lhs">Left side of comparison</param>
        /// <param name="rhs">Right side of comparison</param>
        /// <returns><see langword="false"/> if both handles refer to the same native object (e.g. reference equality)</returns>
        public static bool operator !=( LLVMErrorTypeId lhs, LLVMErrorTypeId rhs ) => !( lhs == rhs );

        /// <summary>Gets a zero (<see langword="null"/>) value handle</summary>
        public static LLVMErrorTypeId Zero { get; } = new LLVMErrorTypeId(IntPtr.Zero);

        /// <summary>Gets the handle as an <see cref="IntPtr"/> suitable for passing to native code</summary>
        /// <returns>The handle as an <see cref="IntPtr"/></returns>
        public IntPtr ToIntPtr() => Handle;

        /// <summary>Gets the handle as an <see cref="IntPtr"/> suitable for passing to native code</summary>
        /// <param name="value">Handle to convert</param>
        /// <returns>The handle as an <see cref="IntPtr"/></returns>
        public static implicit operator IntPtr(LLVMErrorTypeId value) => value.ToIntPtr();

        internal LLVMErrorTypeId( IntPtr p )
        {
            Handle = p;
        }

        private readonly IntPtr Handle;
    }
}
