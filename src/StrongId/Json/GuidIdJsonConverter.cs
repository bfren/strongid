// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System;
using System.Text.Json;

namespace StrongId.Json;

/// <summary>
/// <see cref="IStrongId"/> JSON converter for <see cref="Guid"/> value types
/// </summary>
/// <inheritdoc cref="StrongIdJsonConverter{TId}"/>
public sealed class GuidIdJsonConverter<TId> : StrongIdJsonConverter<TId>
	where TId : class, IStrongId<Guid>, new()
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
				// Handle strings
				JsonTokenType.String =>
					F.ParseGuid(reader.GetString()).Switch(
						some: x => x,
						none: _ => Guid.Empty
					),

				// Handle default
				_ =>
					TrySkip(ref reader, Guid.Empty)
			}
		};
}
