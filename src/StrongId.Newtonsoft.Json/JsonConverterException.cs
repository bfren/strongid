// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System;
using Newtonsoft.Json;

namespace StrongId.Newtonsoft.Json;

/// <summary>
/// Thrown when reading a StrongId JSON value fails -
/// see <see cref="StrongIdJsonConverter{TId}.ReadJson(JsonReader, Type, TId?, bool, JsonSerializer)"/>
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
