// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using Newtonsoft.Json;

namespace StrongId.Newtonsoft.Json;

/// <summary>
/// Read a JSON value as a <see cref="IStrongId"/>
/// </summary>
/// <typeparam name="TId"><see cref="IStrongId"/> type</typeparam>
internal interface IStrongIdJsonReader<TId>
	where TId : class, IStrongId, new()
{
	/// <summary>
	/// Read the current JSON value and convert it to <see cref="TId"/>
	/// </summary>
	/// <param name="reader"></param>
	/// <param name="serializer"></param>
	TId ReadJson(JsonReader reader, JsonSerializer serializer);
}
