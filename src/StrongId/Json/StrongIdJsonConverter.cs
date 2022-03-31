// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System.Text.Json;
using System.Text.Json.Serialization;

namespace StrongId.Json;

/// <summary>
/// <see cref="IStrongId"/> JSON converter
/// </summary>
/// <typeparam name="TId"><see cref="IStrongId"/> type</typeparam>
public abstract class StrongIdJsonConverter<TId> : JsonConverter<TId>
	where TId : class, IStrongId, new()
{
	/// <summary>
	/// Write a <see cref="IStrongId"/> type value
	/// </summary>
	/// <param name="writer"></param>
	/// <param name="value"><see cref="IStrongId"/> value</param>
	/// <param name="options"></param>
	public override void Write(Utf8JsonWriter writer, TId value, JsonSerializerOptions options) =>
		writer.WriteStringValue(value.Value?.ToString());

	/// <summary>
	/// Try to skip the JSON token (because it hasn't been matched correctly) and return a default value
	/// </summary>
	/// <typeparam name="TIdValue"><see cref="IStrongId"/> Value type</typeparam>
	/// <param name="skipped"></param>
	/// <param name="defaultValue"></param>
	/// <exception cref="JsonException"></exception>
	internal TIdValue HandleSkip<TIdValue>(bool skipped, TIdValue defaultValue) =>
		skipped switch
		{
			true =>
				defaultValue,

			_ =>
				throw new JsonException($"Invalid {typeof(TIdValue)} and unable to skip reading current token.")
		};
}
