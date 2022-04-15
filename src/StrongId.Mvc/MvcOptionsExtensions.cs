// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using Microsoft.AspNetCore.Mvc;
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
		@this.ModelBinderProviders.Insert(0, new StrongIdModelBinderProvider());
}
