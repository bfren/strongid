// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System.Text.Json;
using System.Text.Json.Serialization;

namespace StrongId.Json;

/// <summary>
/// <see cref="IStrongId"/> JSON converter
/// </summary>
/// <typeparam name="TId"><see cref="IStrongId"/> type</typeparam>
public abstract class StrongIdConverter<TId> : JsonConverter<TId>
	where TId : class, IStrongId, new()
{
	/// <summary>
	/// Write a <see cref="IStrongId"/> type value
	/// </summary>
	/// <param name="writer">Utf8JsonWriter</param>
	/// <param name="value"><see cref="IStrongId"/> value</param>
	/// <param name="options">JsonSerializerOptions</param>
	public override void Write(Utf8JsonWriter writer, TId value, JsonSerializerOptions options) =>
		writer.WriteStringValue(value.Value?.ToString());

	/// <summary>
	/// Try to skip the JSON token (because it hasn't been matched correctly) and return a default value
	/// </summary>
	/// <typeparam name="TIdValue">StrongId Value type</typeparam>
	/// <param name="reader"></param>
	/// <param name="defaultValue"></param>
	/// <exception cref="JsonException"></exception>
	internal TIdValue TrySkip<TIdValue>(Utf8JsonReader reader, TIdValue defaultValue) =>
		reader.TrySkip() switch
		{
			true =>
				defaultValue,

			_ =>
				throw new JsonException($"Invalid {typeof(TIdValue)} and unable to skip reading current token.")
		};
}
