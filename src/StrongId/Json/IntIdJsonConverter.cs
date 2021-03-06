// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StrongId.Json;

/// <summary>
/// <see cref="IStrongId"/> JSON converter for <see cref="int"/> value types
/// </summary>
/// <inheritdoc cref="StrongIdJsonConverter{TId}"/>
internal sealed class IntIdJsonConverter<TId> : StrongIdJsonConverter<TId>
	where TId : IntId, new()
{
	/// <summary>
	/// Read <see cref="IStrongId"/> type value
	/// </summary>
	/// <param name="reader"></param>
	/// <param name="typeToConvert"><see cref="IStrongId"/> type</param>
	/// <param name="options"></param>
	public override TId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
		new()
		{
			Value = reader.TokenType switch
			{
				// Handle numbers
				JsonTokenType.Number =>
					reader.GetInt32(),

				// Handle strings if strings are allowed
				JsonTokenType.String when (options.NumberHandling & JsonNumberHandling.AllowReadingFromString) != 0 =>
					F.ParseInt32(reader.GetString()).Switch(
						some: x => x,
						none: _ => 0
					),

				// Handle default
				_ =>
					HandleSkip(reader.TrySkip(), 0)
			}
		};
}
