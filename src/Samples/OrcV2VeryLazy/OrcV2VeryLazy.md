---
uid: orcjitv2-very-lazy
---
# ORC JIT v2 Very Lazy sample
This sample is based on the official LLVM C sample but adapted to demonstrate the use of the
Ubiquity.NET.llvm libraries. The sample builds a basic native function that is provided to
the JIT engine. When executed that function calls to an unresolved function. The unresolved
function body is materialized through a delegate that will parse the LLVM IR for the body
to produce the required module. It then "emits" that module to the JIT engine before returning.
This demonstrates how lazy JIT symbol resolution and materializers operate to allow use with
any source. In this sample the source is just LLVM IR in textual form but that is not a
requirement. (It helps to keep the sample as simple as possible without crowding it with parsing
and other language specific cruft. For an example using a custom language AST see [Kaleidoscope Chapter
7.1](xref:Kaleidoscope-ch7.1))
