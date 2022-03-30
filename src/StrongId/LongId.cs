// Mileage Tracker
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using RndF;

namespace StrongId;

/// <summary>
/// Implementation using <see cref="long"/> as the Value type
/// </summary>
public abstract record class LongId : StrongId<long>
{
	/// <summary>
	/// Generate a new <typeparamref name="TId"/> with <paramref name="value"/>
	/// </summary>
	/// <typeparam name="TId">Strong ID type</typeparam>
	/// <param name="value">ID value</param>
	public static TId NewId<TId>(long value)
		where TId : StrongId<long>, new() =>
		new() { Value = value };

	/// <summary>
	/// Generate a new <typeparamref name="TId"/> with a random <see cref="IStrongId.Value"/>
	/// </summary>
	/// <typeparam name="TId">Strong ID type</typeparam>
	public static TId RndId<TId>()
		where TId : StrongId<long>, new() =>
		NewId<TId>(Rnd.Lng);
}
