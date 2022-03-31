// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System;

namespace StrongId;

/// <inheritdoc cref="IStrongId{T}"/>
/// <param name="Value">ID Value</param>
public abstract record class StrongId<T>(T Value) : IStrongId<T>
{
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
