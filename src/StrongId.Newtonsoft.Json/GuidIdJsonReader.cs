// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using Newtonsoft.Json;
using StrongId.Newtonsoft.Json.Functions;

namespace StrongId.Newtonsoft.Json;

/// <summary>
/// <see cref="GuidId"/> JSON reader
/// </summary>
/// <typeparam name="TId"><see cref="GuidId"/> type</typeparam>
internal sealed class GuidIdJsonReader<TId> : IStrongIdJsonReader<TId>
	where TId : GuidId, new()
{
	/// <inheritdoc/>
	public TId ReadJson(JsonReader reader, JsonSerializer serializer) =>
		new()
		{
			Value = reader.TokenType switch
			{
				// Handle strings
				JsonToken.String =>
					F.ParseGuid(reader.ReadAsString()).Switch(
						some: x => x,
						none: _ => Guid.Empty
					),

				// Handle default
				_ =>
					JsonReaderF.HandleSkip(reader.Skip, Guid.Empty)
			}
		};
}
