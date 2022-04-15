// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System;
using StrongId.Mvc.ModelBinding;

namespace StrongId.Mvc.Exceptions;

/// <summary>
/// Thrown when creating a StrongId MVC model binder fails -
/// see <see cref="StrongIdModelBinderProvider.GetBinderFromModelType(Type)"/>
/// </summary>
public sealed class ModelBinderException : Exception
{
	/// <summary>
	/// Create object
	/// </summary>
	public ModelBinderException() { }

	/// <summary>
	/// Create object with message
	/// </summary>
	/// <param name="message"></param>
	public ModelBinderException(string message) : base(message) { }

	/// <summary>
	/// Create object with message and inner exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public ModelBinderException(string message, Exception inner) : base(message, inner) { }
}
