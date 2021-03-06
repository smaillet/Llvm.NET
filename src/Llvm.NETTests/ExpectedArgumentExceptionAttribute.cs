﻿// -----------------------------------------------------------------------
// <copyright file="ExpectedArgumentExceptionAttribute.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Llvm.NETTests
{
    /// <summary>Attribute to mark a method as expecting an argument exception with optional parameter name and exception message validation</summary>
    public sealed class ExpectedArgumentExceptionAttribute
        : ExpectedExceptionBaseAttribute
    {
        public ExpectedArgumentExceptionAttribute( string expectedName )
            : this( expectedName, string.Empty )
        {
        }

        public ExpectedArgumentExceptionAttribute( string expectedName, string noExceptionMessage )
            : base( noExceptionMessage )
        {
            ExpectedName = expectedName;
            WrongExceptionMessage = DefaultWrongExceptionMessage;
        }

        public string WrongExceptionMessage { get; set; }

        public string ExpectedExceptionMessage { get; set; }

        public string ExpectedName { get; }

        public new string NoExceptionMessage => base.NoExceptionMessage;

        [System.Diagnostics.CodeAnalysis.SuppressMessage( "Design", "CA1062:Validate arguments of public methods", Justification = "Validated by Assert" )]
        protected override void Verify( Exception exception )
        {
            Assert.IsNotNull(exception);

            // Handle assertion exceptions from assertion failures in the test method, since we are not interested in verifying those
            RethrowIfAssertException(exception);

            Assert.IsInstanceOfType(exception, typeof(ArgumentException), WrongExceptionMessage );
            var argException = ( ArgumentException )exception;
            Assert.AreEqual( ExpectedName, argException.ParamName );
            if( !string.IsNullOrWhiteSpace( ExpectedExceptionMessage ) )
            {
                Assert.IsTrue( exception.Message.StartsWith( ExpectedExceptionMessage, StringComparison.Ordinal ), "Could not verify the exception message." );
            }
        }

       private const string DefaultWrongExceptionMessage = "The exception that was thrown does not derive from System.ArgumentException.";
    }
}
