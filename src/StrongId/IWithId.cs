// Mileage Tracker
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId;

/// <summary>
/// Represents an object (Entity or Model) with a strongly-typed ID
/// </summary>
public interface IWithId
{
	/// <summary>
	/// IStrongId object wrapping an ID Value
	/// </summary>
	IStrongId Id { get; init; }
}

/// <inheritdoc cref="IWithId"/>
/// <typeparam name="T">IStrongId Type</typeparam>
public interface IWithId<T> : IWithId
	where T : class, IStrongId, new()
{
	/// <summary>
	/// IStrongId object of type <typeparamref name="T"/> wrapping an ID Value
	/// </summary>
	new T Id { get; init; }

	/// <inheritdoc/>
	IStrongId IWithId.Id
	{
		get => Id;
		init => Id = value switch
		{
			T id =>
				id,

			_ =>
				throw new InvalidCastException($"Unable to set ID to {value}: expecting an object of type {typeof(T)}.")
		};
	}
}
