// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using Newtonsoft.Json;
using StrongId.Newtonsoft.Json.Functions;

namespace StrongId.Newtonsoft.Json;

/// <summary>
/// <see cref="IntId"/> JSON reader
/// </summary>
/// <typeparam name="TId"><see cref="IntId"/> type</typeparam>
internal sealed class IntIdJsonReader<TId> : IStrongIdJsonReader<TId>
	where TId : IntId, new()
{
	/// <inheritdoc/>
	public TId ReadJson(JsonReader reader, JsonSerializer serializer) =>
		new()
		{
			Value = reader.TokenType switch
			{
				// Handle numbers
				JsonToken.Integer =>
					reader.ReadAsInt32() ?? 0,

				// Handle strings
				JsonToken.String =>
					F.ParseInt32(reader.ReadAsString()).Switch(
						some: x => x,
						none: _ => 0
					),

				// Handle default
				_ =>
					JsonReaderF.HandleSkip(reader.Skip, 0)
			}
		};
}
