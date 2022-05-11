// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System;
using Newtonsoft.Json;
using StrongId.Newtonsoft.Json.Functions;

namespace StrongId.Newtonsoft.Json;

/// <summary>
/// <see cref="GuidId"/> JSON reader
/// </summary>
/// <typeparam name="TId"><see cref="GuidId"/> type</typeparam>
internal sealed class GuidIdJsonReader<TId> : IStrongIdJsonReader
	where TId : GuidId, new()
{
	/// <inheritdoc/>
	public IStrongId ReadJson(JsonReader reader, JsonSerializer serializer) =>
		new TId()
		{
			Value = reader.Value?.ToString() switch
			{
				// Parse string or use default
				string s =>
					F.ParseGuid(s).Switch(
						some: x => x,
						none: _ => Guid.Empty
					),

				// Skip value and use default
				_ =>
					JsonReaderF.HandleSkip(reader.Skip, Guid.Empty)
			}
		};
}
