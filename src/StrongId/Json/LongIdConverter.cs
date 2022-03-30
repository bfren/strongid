// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StrongId.Json;

/// <summary>
/// <see cref="IStrongId"/> JSON converter for <see cref="long"/> value types
/// </summary>
/// <inheritdoc cref="StrongIdConverter{TId, TIdValue}"/>
public sealed class LongIdConverter<TId> : StrongIdConverter<TId>
	where TId : class, IStrongId<long>, new()
{
	/// <summary>
	/// Read <see cref="IStrongId"/> type value
	/// </summary>
	/// <param name="reader">Utf8JsonReader</param>
	/// <param name="typeToConvert"><see cref="IStrongId"/> type</param>
	/// <param name="options">JsonSerializerOptions</param>
	/// <exception cref="JsonException"></exception>
	public override TId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
		new()
		{
			Value = reader.TokenType switch
			{
				// Handle numbers
				JsonTokenType.Number =>
					reader.GetInt64(),

				// Handle strings if strings are allowed
				JsonTokenType.String when (options.NumberHandling & JsonNumberHandling.AllowReadingFromString) != 0 =>
					F.ParseInt64(reader.GetString()).Switch(
						some: x => x,
						none: _ => 0L
					),

				// Handle default
				_ =>
					TrySkip(reader, 0L)
			}
		};
}
