// -----------------------------------------------------------------------
// <copyright file="LibraryInitTests.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ubiquity.NET.Llvm.Interop.ABI.libllvm_c;

namespace Ubiquity.NET.Llvm.Interop.UT
{
    [TestClass]
    public class LibraryInitTests
    {
        [DistinctProcessTestMethod]
        public void TestLibraryInit( )
        {
            using var lib = Library.InitializeLLVM();
            Assert.IsNotNull(lib);
        }

        [DistinctProcessTestMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage( "Style", "IDE0063:Use simple 'using' statement", Justification = "Explicit scoping helps make usage more clear" )]
        public void TestLibraryReInit( )
        {
            using(var lib = Library.InitializeLLVM())
            {
                Assert.IsNotNull(lib);
            }

            using(var lib2 = Library.InitializeLLVM())
            {
                Assert.IsNotNull(lib2);
            }
        }
    }
}
