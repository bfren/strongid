// StrongId: Strongly-Typed ID Values
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
	/// ID Value
	/// </summary>
	object Value { get; init; }
}

/// <summary>
/// Represents a strongly-typed ID with a custom ID value type
/// </summary>
/// <typeparam name="T"><see cref="IStrongId"/> Value type</typeparam>
internal interface IStrongId<T> : IStrongId
{
	/// <inheritdoc cref="IStrongId.Value"/>
	new T Value { get; init; }
}
