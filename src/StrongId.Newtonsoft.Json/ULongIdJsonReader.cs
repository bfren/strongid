// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using Newtonsoft.Json;
using StrongId.Newtonsoft.Json.Functions;

namespace StrongId.Newtonsoft.Json;

/// <summary>
/// <see cref="LongId"/> JSON reader
/// </summary>
/// <typeparam name="TId"><see cref="LongId"/> type</typeparam>
internal class ULongIdJsonReader<TId> : IStrongIdJsonReader
	where TId : ULongId, new()
{
	/// <inheritdoc/>
	public IStrongId ReadJson(JsonReader reader, JsonSerializer serializer) =>
		new TId()
		{
			Value = reader.Value?.ToString() switch
			{
				// Parse string or use default
				string s =>
					F.ParseUInt64(s).Switch(
						some: x => x,
						none: _ => 0UL
					),

				// Skip value and use default
				_ =>
					JsonReaderF.HandleSkip(reader.Skip, 0UL)
			}
		};
}
