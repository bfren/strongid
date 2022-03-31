// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using StrongId.Functions;

namespace StrongId.Json;

/// <summary>
/// <see cref="IStrongId"/> JSON converter factory
/// </summary>
public sealed class StrongIdJsonConverterFactory : JsonConverterFactory
{
	/// <summary>
	/// Returns true if <paramref name="typeToConvert"/> inherits from <see cref="IStrongId"/>
	/// </summary>
	/// <param name="typeToConvert"><see cref="IStrongId"/> type</param>
	public override bool CanConvert(Type typeToConvert) =>
		TypeF.GetStrongIdValueType(typeToConvert) is not null;

	/// <summary>
	/// Creates JsonConverter using <see cref="IStrongId"/> type as generic argument
	/// </summary>
	/// <param name="typeToConvert"><see cref="IStrongId"/> type</param>
	/// <param name="options"></param>
	/// <exception cref="JsonConverterException"></exception>
	public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		// IStrongId<> requires one type argument
		var strongIdValueType = TypeF.GetStrongIdValueType(typeToConvert);
		if (strongIdValueType is null)
		{
			throw new JsonConverterException($"{typeToConvert} does not implement {typeof(IStrongId<>)}.");
		}

		// Use the Value type to determine which converter to use
		var strongIdConverter = strongIdValueType switch
		{
			Type t when t == typeof(Guid) =>
				typeof(GuidIdJsonConverter<>),

			Type t when t == typeof(int) =>
				typeof(IntIdJsonConverter<>),

			Type t when t == typeof(long) =>
				typeof(LongIdJsonConverter<>),

			{ } t =>
				throw new JsonConverterException($"StrongId with value type {t} is not supported.")
		};

		// Attempt to create and return the converter
		var genericType = strongIdConverter.MakeGenericType(typeToConvert);
		return Activator.CreateInstance(genericType) switch
		{
			JsonConverter x =>
				x,

			_ =>
				throw new JsonConverterException($"Unable to create {typeof(StrongIdJsonConverter<>)} for type {typeToConvert}."),
		};
	}
}
