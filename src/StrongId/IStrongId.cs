// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System;

namespace StrongId;

/// <summary>
/// Represents a strongly-typed ID - this should never be implemented directly -
/// see <see cref="GuidId"/>, <see cref="IntId"/>, and <see cref="LongId"/>
/// </summary>
/// <remarks>
/// This exists only to enable generic querying and parsing of values
/// </remarks>
public interface IStrongId
{
	/// <summary>
	/// ID Value
	/// </summary>
	object Value { get; init; }
}

/// <summary>
/// Represents a strongly-typed ID with a custom ID value type - see <see cref="GuidId"/>,
/// <see cref="IntId"/>, and <see cref="LongId"/> for implementations
/// </summary>
/// <typeparam name="T"><see cref="IStrongId"/> Value type</typeparam>
internal interface IStrongId<T> : IStrongId
{
	/// <inheritdoc cref="IStrongId.Value"/>
	new T Value { get; init; }

	/// <summary>
	/// Implement <see cref="IStrongId.Value"/> explicitly to enable the type-specific override
	/// </summary>
	/// <exception cref="InvalidCastException"></exception>
	object IStrongId.Value
	{
		get => Value ?? new object();
		init => Value = value switch
		{
			T v =>
				v,

			_ =>
				throw new InvalidCastException($"Unable to set ID value to {value}: expecting an object of type {typeof(T)}.")
		};
	}
}
