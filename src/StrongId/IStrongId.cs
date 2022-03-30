// Mileage Tracker
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId;

/// <summary>
/// Represents a strongly-typed ID - this should never be implemented directly - see <see cref="IStrongId{T}"/>
/// </summary>
/// <remarks>
/// This exists only to enable generic querying and parsing of values
/// </remarks>
public interface IStrongId
{
	/// <summary>
	/// ID value
	/// </summary>
	object Value { get; init; }
}

/// <summary>
/// Represents a strongly-typed ID with a custom ID value type
/// </summary>
/// <typeparam name="T">StrongId Value type</typeparam>
public interface IStrongId<T> : IStrongId
{
	/// <inheritdoc/>
	new T Value { get; init; }

	/// <summary>
	/// Implement value explicitly to enable the type-specific override
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
