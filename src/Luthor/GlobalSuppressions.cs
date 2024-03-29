﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "these fields are readonly static, which is basically const")]
[assembly: SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "readonly fields are faster than properties")]
