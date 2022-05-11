// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System;
using Newtonsoft.Json;

namespace StrongId.Newtonsoft.Json.Functions;

/// <summary>
/// <see cref="JsonReader"/> helper functions
/// </summary>
public static class JsonReaderF
{
	/// <summary>
	/// Allows skipping the current tree and returning a default value - see e.g.
	/// <see cref="LongIdJsonReader{TId}.ReadJson(JsonReader, JsonSerializer)"/>
	/// </summary>
	/// <typeparam name="TIdValue"></typeparam>
	/// <param name="skip"></param>
	/// <param name="defaultValue"></param>
	public static TIdValue HandleSkip<TIdValue>(Action skip, TIdValue defaultValue)
	{
		skip();
		return defaultValue;
	}
}
