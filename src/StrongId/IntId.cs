// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using RndF;

namespace StrongId;

/// <summary>
/// Implementation using <see cref="int"/> as the Value type
/// </summary>
public abstract record class IntId : StrongId<int>
{
	/// <summary>
	/// Generate a new <typeparamref name="TId"/> with <paramref name="value"/>
	/// </summary>
	/// <typeparam name="TId">Strong ID type</typeparam>
	/// <param name="value">ID value</param>
	public static TId NewId<TId>(int value)
		where TId : StrongId<int>, new() =>
		new() { Value = value };

	/// <summary>
	/// Generate a new <typeparamref name="TId"/> with a random <see cref="IStrongId.Value"/>
	/// </summary>
	/// <typeparam name="TId">Strong ID type</typeparam>
	public static TId RndId<TId>()
		where TId : StrongId<int>, new() =>
		NewId<TId>(Rnd.Int);
}
