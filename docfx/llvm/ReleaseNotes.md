# Release Notes
# V20.1.8.Alpha
Major re-work to support LLVM 20 and .NET 9 and later with focus on performance and heading towards AOT.
Major changes include:
* OrcJIT v2 support
    - Including functioning lazy materialization from the AST
        - Even on Microsoft Windows! :wink:
* Opaque pointers
    - Underlying LLVM uses only opaque pointers however these wrappers account for
      that as much as possible without significant impact on calling code. The wrappers,
      when used with debug information, support tracking the LLVM type of the `pointee`
      for you in most cases. Though if not using any debug information or otherwise dealing
      in the raw types applications will need to keep track of the type of a pointer
      instead of relying on the LLVM IR to do that for you.
* Dropped reference equality to support multi-threaded nature of OrcJIT.
    - Things got complicated and broke around chapter 5 of the Kaleidoscope tutorials.
      The basic problem with interning is that it doesn't account for ownership. In fact
      it downright ignores the point. This is a serious problem when dealing with a
      multi-threaded JIT engine as you might end up disposing something you own that
      was transferred to the native API or worse an alias is resolved to an owned
      instance which is then destroyed - OOPS! [Bad idea - seemed like a good idea
      at the time! :facepalm: ]
* Consumers need to consider IDispose and "ownership" in general
    - Usually this is as simple as a `using` statement to properly handle
      cleanup in scope if there is an exception. Sometimes it takes a bit more
      thought to handle properly. (.NET, C# and the general community have no concept of the
      common native code "move semantics" pattern)
* A significant impact of ownership is in the LLVM transition to DebugRecords
    - While the core API has not changed, the ownership of the classes has and
      in particular that a DIBuilder is now a "ref like" type (`ref struct` in C#).
        - Such a type can reference the module and compile unit but is NOT owned by
          the module and therefore not exposed as a property for it.

## Breaking changes
This is a major release and there are a LOT of changes though they are all fairly
small. Ownership is the big change as things have changed in the underlying LLVM and
this library has shifted to NOT support cached instances as it confuses the question
of ownership (more specifically the release of resources). A look at the samples and
test code will show that the core of the library didn't change but some things about
how you use it did. Additional support packages were created to move common re-usable
code out of the samples and now exist as part of the same release. [See: the [index
for these docs](../index.md) for more details]

### New projects
If you are just starting out and not upgrading to this release then
1) Welcome to the party!
2) No need to worry about these major changes.

# v10.0.0

## Breaking changes
With the 10.* release the Ubiquity.NET.Llvm.* libs have made a number of breaking changes.
While these are mostly small and easily adapted to, they are still a breaking change. Thus,
these changes were held to only occur on a Major release. Despite the pain of updating code
we think the changes are worth the effort to create a cleaner simpler and more consistent library.

### Library name changes
With the 10.* release the names of the assemblies, and therefore the packages, are all changed.
This was done to unify them all under a common organization name to allow use of the facilities
provided by NuGet for organizations and to help clarify these libraries from some other similar
projects no longer maintained.

| Old Name                  | New Name     |
|---------------------------|--------------|
| Ubiquity.NET.Llvm.Interop | Ubiquity.Net.Llvm.Interop |
| LibLLVM.dll               | Ubiquity.Net.LibLLVM      |
| Ubiquity.NET.Llvm         | Ubiquity.Net.Llvm         |

### Library initialization
The library initialization and target registration was changed to better reflect the requirements
and proper sequence of usage. In particular the `Ubiquity.Net.Llvm.Interop.Library.InitializeLLVM()`
static function now returns an `ILibLLVM` interface (that is still disposable). All target registration
is done through this interface. This prevents accidental use of the registration methods **BEFORE**
initializing the library (as that's a guaranteed app crash!)

### C#8 and non-Nullable references
With the 10.* release the Ubiquity.NET.Llvm.* libs all updated to target .NET Standard 2.1 and C#8. This allows
use of nullable types to make nullability more explicit. This necessitated a few minor breaking changes in the
object model surface.

| Name            | Description  |
|-----------------|--------------|
| DebugMemberInfo | Removed setters of non-nullable properties and added constructor to allow building the type with non-null values | 


### Renamed instruction predicate enumerator values
The comparison instruction predicates `Ubiquity.NET.Llvm.Instructions.[Predicate|IntPredicate]`were renamed for greater
consistency and clarity (Some of the float predicates had 'Than' in the name while the integer counterparts did not. (See:
[Bug #152](https://github.com/UbiquityDotNET/Llvm.NET/issues/152) for details.)

| Old Name               | New Name     |
|------------------------|--------------|
| UnsignedGreater        | UnsignedGreaterThan |
| UnsignedGreaterOrEqual | UnsignedGreaterThanOrEqual |
| UnsignedLess           | UnsignedLessThan |
| UnsignedLessOrEqual    | UnsignedLessThanOrEqual |
| SignedGreater          | SignedGreaterThan |
| SignedGreaterOrEqual   | SignedGreaterThanOrEqual |
| SignedLess             | SignedLessThan |
| SignedLessOrEqual      | SignedLessThanOrEqual |

### Renamed APIs
Some APIs had inconsistent, misspelled or confusing names and were updated.

| Old Name               | New Name     |
|------------------------|--------------|
| `Ubiquity.NET.Llvm.Transforms.ScalarTransforms.LowerAtomicPass<T>` | `Ubiquity.NET.Llvm.Transforms.ScalarTransforms.AddLowerAtomicPass<T>` |

### Obsoleted APIs
| Obsolete API              | Alternative API                | Justification |
|---------------------------|--------------------------------|---------------|
| Module.AddFunction | Module.CreateFunction() | The Create vs Add between debug info and raw native was always confusing |

### Types no longer disposable
Some types are no longer disposable. The LLVMObjectRef type uses SafeHandle, which eliminates the need
for Dispose for top level global objects. The following types no longer support IDispose:
* Transforms.ModulePassManager
* Transforms.FunctionPassManager
* ObjectFile.TargetBinary

### Altered Behavior
#### Context.CreateStructType()
As part of resolving [bug #184](https://github.com/UbiquityDotNET/Llvm.NET/issues/184) the CreateStructType
methods were re-evaluated and found lacking in functionality (the bug) and clarity. The docs were misleading
and the implementations overly restrictive in some cases. Thus these have been re-worked to make it more clear
when a Sized vs. Opaque structure type is created, in particular, for **ALL** overloads taking a 'packed' parameter
a sized type is created, even if the size is 0 because no members are provided. This allows creation of named or 
anonymous empty structs, used in many languages. To create a named opaque type then the overload with just the
name is used. This isn't expected to impact many consumers, other than the tests, but it is a breaking change.

#### Context.CreateConstantString()
The behavior of Context.CreateConstantString(string) has changed slightly. It now constructs a valid C string with
a null terminator, which is generally what would be expected of something called "string". (The (string,bool)
overload remains, to allow apps to be explicit with intent) Additionally, the ConstantDataSequential.IsString
property now reflects whether the string is a C string (terminating null but no embedded nulls) and the
ConstantDataSequential.IsI8Sequence was added to provide the previous behavior of IsString, which was simply that
the underlying sequence element type was i8 (with or without a terminator)

### Removed redundant APIs
LLVM has made additional APIs available in the standard LLVM-C library that are either identical to or functionality
equivalent to APIs that were custom in previous versions of the Ubiquity.NET.Llvm DLLs. This is only observable at
the interop library layer where some of the custom APIs were removed and replaced with the official ones.

| Removed custom API | New Official API |
|--------------------|------------------|
| LibLLVMFoo [TBD]   | LLVMFoo [TBD]    |

### Disabled ORCJIT LazyFunction binding
Unfortunately, the ORCJIT truly lazy function generation callback support is currently disabled. LLVM itself is
transitioning to the ORCJIT v2 and in the process broke the lazy function binding support (At least for Windows+COFF).
Previously a workaround for the issue of the COFF exports was applied in the Llvm.NET ORCJIT library code for symbol
lookups. However, with ORCJIT v2 the JIT itself is doing lookups and it does so only for external symbols assuming the
symbols it generates internally will be exports, but are not (at least for COFF modules anyway). For more details see
the LLVM bugs [25493](https://bugs.llvm.org/show_bug.cgi?id=25493) and [28699](https://bugs.llvm.org/show_bug.cgi?id=28699)

## v8.0.1
### Bug Fixes


| Bug   | Description  |
|-------|--------------|
| [151](https://github.com/UbiquityDotNET/Llvm.NET/issues/151) | Updated DebugFunctionType signature to use interface instead of concrete type |
| [152](https://github.com/UbiquityDotNET/Llvm.NET/issues/152) | Corrected docs copy/paste error [renaming part of the issue is left for the next major release as that is a breaking change] |

### Additional changes
Additionally the internal build scripts were updated to simplify the consistent cross solution versioning. Previously,
a complex process of building a dummy project to generate a data file was used, however that was no longer necessary
as the [CSemVer.Build.Tasks ](https://github.com/UbiquityDotNET/CSemVer.GitBuild) package can figure out all except
the CiBuildIndex, which, for this project, is an ISO-8601 formatted time-stamp (of the latest commit for automated
builds or the build start for local developer builds)

## v8.0.0
### Ubiquity.NET.Llvm.Interop (New library)
Ubiquity.NET.Llvm 8.0 adds a new library (Ubiquity.NET.Llvm.Interop)  that contains the raw P/Invoke
APIs and support needed to inter-operate with the native library. The NuGet package
for the interop library includes the native code binaries as they are tightly coupled.
This package contains the native LibLLVM.dll and the P/Invoke interop support layers.
Ubiquity.NET.Llvm uses this library to define a clean projection of LLVM for .NET consumers.
This will, hopefully, allow for future development and enhancement of the Ubiquity.NET.Llvm
object model without changing the underlying P/Invoke layers. (e.g.
the Ubiquity.NET.Llvm.Interop can "snap" to LLVM versions, but the Ubiquity.NET.Llvm model can have
multiple incremental releases) This isn't a hard/fast rule as it is possible that
getting new functionality in the object model requires new custom extensions. At
this point in time both libraries are built together and share build numbers.
Though, that may change in the future. 

#### Auto-generated P/Invoke
LLVM-C API now includes most of the debug APIs so, significantly fewer custom
extensions are needed (That's a good thing!). To try and keep things simpler this
moves the interop back to using code generation for the bulk of the P/Invoke interop.
However, unlike the first use of generation, the [LLVMBindingsGenerator](https://github.com/UbiquityDotNET/Llvm.NET/tree/master/src/Interop/LlvmBindingsGenerator)
is much more targeted and includes specialized handling to prevent the need for
additional "by-hand" tweaking of the generated code, such as:

1. Marshaling of strings with the many ways to dispose (or not) a returned string
2. LLVMBool vs LLVMStatus
3. "smart ref" handle types, including aliases that should not be released by
   client code.

The generated code is combined with some fixed support classes to create a new
Ubiquity.NET.Llvm.Interop Library and NuGet Package. 

### New features
* ObjectFile Support
  * Ubiquity.NET.Llvm.ObjectFile namespace contains support for processing object files using LLVM
* Eager compilation JIT
  * The OrcJIT now supports eager and lazy compilation for Windows platforms
* Full initialization for all the latests supported targets
  * Including - BPF, Lanai, WebAssembly, MSP430, NVPTX, AMDGPU, Hexagon, and XCore
* Added accessors to allow retrieval/addition of metadata on instructions

### Breaking Changes
This is a Major release and, as such, can, and does, have breaking changes. While there
are several such changes the actual impact to a code base is fairly trivial. Most are
driven by either obsolescence of functionality in LLVM or general naming cleanup in the
Ubiquity.NET.Llvm library:

1. New namespace and assembly for some classes (Ubiquity.NET.Llvm.Interop)
    1. Ubiquity.NET.Llvm.DisposableAction -> Ubiquity.NET.Llvm.Interop.DisposableAction
    2. Ubiquity.NET.Llvm.DisposableObject -> Ubiquity.NET.Llvm.Interop.DisposableObject
    3. Ubiquity.NET.Llvm.StaticState -> Ubiquity.NET.Llvm.Interop.Library
    4. Ubiquity.NET.Llvm.TargetRegistrations -> Ubiquity.NET.Llvm.Interop.TargetRegistrations
2. StaticState class is renamed to Ubiquity.NET.Llvm.Interop.Library as it is fundamentally 
   part of the low level interop (and "StaticState" was always a bad name)
3. Instructions no longer have a SetDebugLocation, instead that is provided via a new
   fluent method on the InstructionBuilder since the normal use is to set the location
   on the builder and then generate a sequence of IR instructions for a given expression
   in code. 
4. Legacy JIT engine support is dropped. ORCJit is the only supported JIT engine
    1. Removed Ubiquity.NET.Llvm.JIT.EngineKind
    2. Removed all use of Ubiquity.NET.Llvm.JIT.IJitModuleHandle. Handles are now just an integral value
    3. Removed Ubiquity.NET.Llvm.LegacyExecutionEngine
5. Context.CreateBasicBlock() now only creates detached blocks, if append to a function
   is desired, there is a method on IrFunction to create and append a block.
    1. CreateBasicBlock signature changed to remove the function and block parameters 
6. PassManager, ModulePassManager, and FunctionPassManager are IDisposable to help apps
   ensure that a function pass manager, which is bound to a module, is destroyed before
   the module it is bound to. Failure to do so can result in app crashes from access
   violations in the native LLVM code.
7. Module
    1. MakeShared and shared refs of modules is removed. (This was created for
        OrcJIT use of shared_ptr under the hood, which is no longer used. OrcJit now uses the
        same ownership transfer model as the legacy engines. E.g. the ownership for the module
        is transferred to the JIT engine)
    2. Module is now Disposable backed by a safe handle, this allows for detaching and
       invalidating the underlying LLVMModuleRef when the module is provided to the JIT
    3. CreateFunction() signature changed, Dropped the default null node parameters
       not supported by the LLVM-C implementation.

8. Renamed Function class to IrFunction to avoid potential collision with common language
   keywords
9. Renamed Select to SelectInstruction to avoid potential collision with language keyword
    and make consistent with ReturnInstruction, ResumeInstruction and other similar cases
    for instructions.
10. Removed transform pass functions not supported in LLVM-C
    1. SclaraTransforms.AddLateCFGSimplificationPass()
11. `GlobalValueExtensions.UnnameAddress<T>(T,bool)` was changed to
    `GlobalValueExtensions.UnnameAddress<T>(T,UnnamedAddressKind)` to support changes in
    underlying LLVM
12. Removed ValueExtensions.SetDugLocation() [All overloaded forms], debug location is set
    in the InstructionBuilder and remains in effect for all instructions until reset or
    cleared by setting it to null.
13. DIBuilder
    1. CreateFunction() signature changed, Dropped the default null node parameters
       not supported by the LLVM-C implementation.
    2. DIBuilder.CreateReplaceableCompositeType() and CreateUnionType() signatures changed to
       include unique ID
       1. The id is set to default to string.Empty so this should largely go without actually
          breaking anything
    3. CreateBasicType Added DebugIngoFlags parameter
    4. CreateEnumerationType removed uniqueId string parameter as it isn't supported by LLVM-C
    5. Obsoleted CreateStructType signature taking `DINodeArray` in favor of `IEnumerable<DINode>`
14. Ubiquity.NET.Llvm.DebugInfo.ExpressionOp names changed to correct PascalCasing and eliminate
    underscores in the value names for better consistency and style compliance.
15. Renamed some Ubiquity.NET.Llvm.DebugInfo.SourceLanguage vendor specific values to conform with
    underlying LLVM names
    1. RenderScript -> GoogleRenderScript
    2. Delphi -> BorlandDelphi
16. Renamed or removed some of the Ubiquity.NET.Llvm.DebugInfo.Tag values to better reflect underlying
    LLVM names and avoid potential language keyword conflicts.
    1. Label -> TagLabel
    2. PtrToMemberType -> PointerToMemberType
    3. Removed AutoVariable, ArgVariable, Expression, UserBase, LoUser and MipsLoop as they
       don't exist in the LLVM support.
17. InstructionBuilder
    1. Obsoleted Methods that don't support opaque pointers in preparation for LLVM's transition
    2. Changed MemCpy, MemMove, and MemSet signatures to remove alignment as LLVM intrinsic no
       longer includes an alignment parameter. It is applied as a parameter attribute for each 
       of the pointer parameters (source and destination).
18. Ubiquity.NET.Llvm.JIT.IExecutionEngine
    1. Replaced AddModule with AddEagerlyCompiledModule to make it more explicit on the behavior
19. Ubiquity.NET.Llvm.ILazyCompileExecutionEngine
    1. Replaced AddModule [From IExecutionEngine] with AddLazyCompiledModule to make it explicit
    2. Removed DefalultSymbolResolver from interface as it should not have been in the interface
       to start with.
20. Deleted Ubiquity.NET.Llvm.LegacyExecutionEngine
21. Ubiquity.NET.Llvm.JIT.OrcJit - updated to reflect changes in the IExecutionEngine and
    ILazyCompileExecutionEngine interfaces.

