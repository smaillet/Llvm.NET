﻿// -----------------------------------------------------------------------
// <copyright file="IRBindingsTests.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ubiquity.NET.Llvm.Interop.ABI.libllvm_c.UT
{
    [TestClass]
    public class IRBindingsTests
    {
        [TestMethod]
        [Ignore("Not yet implemented")]
        public void LibLLVMHasUnwindDestTest( )
        {
            // As of this writing, the only implemented instructions that might contain an unwind dest are a CleanupReturn and
            // CatchSwitchInst, all other instructions result in a 0;
            // TODO: Figure out minimum API calls needed to build a valid case for each type and at least one for the "negative"
            //       then validate the API returns the correct value.
        }
    }
}
