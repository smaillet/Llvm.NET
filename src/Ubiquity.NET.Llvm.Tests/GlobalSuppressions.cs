﻿// -----------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Ubiquity.NET Contributors">
// Copyright (c) Ubiquity.NET Contributors. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

/* This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
*/

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage( "StyleCop.CSharp.DocumentationRules", "SA1652:Enable XML documentation output", Justification = "Test Code" )]
[assembly: SuppressMessage( "StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Test Code" )]
[assembly: SuppressMessage( "Naming", "CA1707:Identifiers should not contain underscores", Justification = "Tests method names are intended to be descriptive" )]
