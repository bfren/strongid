// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System;
using Newtonsoft.Json;
using StrongId.Functions;

namespace StrongId.Newtonsoft.Json;

/// <summary>
/// <see cref="IStrongId"/> JSON converter
/// </summary>
/// <typeparam name="TId"><see cref="IStrongId"/> type</typeparam>
public sealed class StrongIdJsonConverter<TId> : JsonConverter<TId>
	where TId : class, IStrongId, new()
{
	/// <inheritdoc/>
	public override TId? ReadJson(JsonReader reader, Type objectType, TId? existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		// StrongId<> requires one type argument
		var strongIdValueType = TypeF.GetStrongIdValueType(objectType);
		if (strongIdValueType is null)
		{
			throw new JsonConverterException(
				$"{objectType} is an invalid {typeof(IStrongId)}: " +
				"please implement one of the provided abstract ID record types."
			);
		}

		// Ensure there is a parameterless contstructor
		if (objectType.GetConstructor(Array.Empty<Type>()) is null)
		{
			throw new JsonConverterException(
				$"{objectType} does not have a parameterless constructor."
			);
		}

		// Use the Value type to determine which reader to use
		var strongIdReader = strongIdValueType switch
		{
			Type t when t == typeof(Guid) =>
				typeof(GuidIdJsonReader<>),

			Type t when t == typeof(int) =>
				typeof(IntIdJsonReader<>),

			Type t when t == typeof(long) =>
				typeof(LongIdJsonReader<>),

			{ } t =>
				throw new JsonConverterException(
					$"StrongId with value type {t} is not supported."
				)
		};

		// Attempt to create and return the reader
		var genericType = strongIdReader.MakeGenericType(objectType);
		return Activator.CreateInstance(genericType) switch
		{
			IStrongIdJsonReader<TId> x =>
				x.ReadJson(reader, serializer),

			_ =>
				throw new JsonConverterException(
					$"Unable to create {typeof(IStrongIdJsonReader<>)} for type {objectType}."
				)
		};
	}

	/// <inheritdoc/>
	public override void WriteJson(JsonWriter writer, TId? value, JsonSerializer serializer) =>
		writer.WriteValue(value?.Value);
}
