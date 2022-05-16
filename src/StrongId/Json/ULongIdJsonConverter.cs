// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StrongId.Json;

/// <summary>
/// <see cref="IStrongId"/> JSON converter for <see cref="ulong"/> value types
/// </summary>
/// <inheritdoc cref="StrongIdJsonConverter{TId}"/>
internal sealed class ULongIdJsonConverter<TId> : StrongIdJsonConverter<TId>
	where TId : ULongId, new()
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
					reader.GetUInt64(),

				// Handle strings if strings are allowed
				JsonTokenType.String when (options.NumberHandling & JsonNumberHandling.AllowReadingFromString) != 0 =>
					F.ParseUInt64(reader.GetString()).Switch(
						some: x => x,
						none: _ => 0UL
					),

				// Handle default
				_ =>
					HandleSkip(reader.TrySkip(), 0UL)
			}
		};
}
