// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System;

namespace StrongId.Json;

/// <summary>
/// Thrown when creating a StrongId JSON converter fails -
/// see <see cref="StrongIdJsonConverterFactory.CreateConverter(Type, System.Text.Json.JsonSerializerOptions)"/>
/// </summary>
public sealed class JsonConverterException : Exception
{
	/// <summary>
	/// Create object
	/// </summary>
	public JsonConverterException() { }

	/// <summary>
	/// Create object with message
	/// </summary>
	/// <param name="message"></param>
	public JsonConverterException(string message) : base(message) { }

	/// <summary>
	/// Create object with message and inner exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public JsonConverterException(string message, Exception inner) : base(message, inner) { }
}
