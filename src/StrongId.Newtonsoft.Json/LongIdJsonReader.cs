// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using Newtonsoft.Json;
using StrongId.Newtonsoft.Json.Functions;

namespace StrongId.Newtonsoft.Json;

/// <summary>
/// <see cref="LongId"/> JSON reader
/// </summary>
/// <typeparam name="TId"><see cref="LongId"/> type</typeparam>
internal class LongIdJsonReader<TId> : IStrongIdJsonReader<TId>
	where TId : LongId, new()
{
	/// <inheritdoc/>
	public TId ReadJson(JsonReader reader, JsonSerializer serializer) =>
		new()
		{
			Value = reader.TokenType switch
			{
				// Handle numbers
				JsonToken.Integer =>
					reader.ReadAsInt32() ?? 0L,

				// Handle strings
				JsonToken.String =>
					F.ParseInt64(reader.ReadAsString()).Switch(
						some: x => x,
						none: _ => 0L
					),

				// Handle default
				_ =>
					JsonReaderF.HandleSkip(reader.Skip, 0L)
			}
		};
}
