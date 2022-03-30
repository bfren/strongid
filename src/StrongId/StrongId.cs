// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId;

/// <inheritdoc cref="IStrongId{T}"/>
/// <param name="Value">ID value</param>
public abstract record class StrongId<T>(T Value) : IStrongId<T>
	where T : new()
{
	/// <summary>
	/// A parameterless constructor is required for data / MVC model binding to work
	/// </summary>
	protected StrongId() : this(new T()) { }
}
