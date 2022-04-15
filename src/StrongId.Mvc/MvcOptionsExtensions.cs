// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StrongId.Mvc.ModelBinding;

namespace StrongId.Mvc;

/// <summary>
/// <see cref="MvcOptions"/> extension methods
/// </summary>
public static class MvcOptionsExtensions
{
	/// <summary>
	/// Insert <see cref="StrongIdModelBinderProvider"/> into the MVC options
	/// </summary>
	/// <param name="this"></param>
	public static void AddStrongIdModelBinder(this MvcOptions @this) =>
		InsertProvider(@this.ModelBinderProviders.Insert);

	/// <summary>
	/// Abstract inserting provider to enable testing
	/// </summary>
	/// <param name="insert"></param>
	internal static void InsertProvider(Action<int, IModelBinderProvider> insert) =>
		insert(0, new StrongIdModelBinderProvider());
}
