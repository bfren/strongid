// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using RndF;

namespace StrongId;

/// <summary>
/// Implementation using <see cref="long"/> as the Value type
/// </summary>
public abstract record class LongId : StrongId<long>
{
	/// <summary>
	/// Create ID with default value
	/// </summary>
	protected LongId() : base(0L) { }

	/// <summary>
	/// Create ID with value
	/// </summary>
	/// <param name="value">ID Value</param>
	protected LongId(long value) : base(value) { }

	/// <summary>
	/// Generate a new <typeparamref name="TId"/> with a random <see cref="IStrongId.Value"/>
	/// </summary>
	/// <typeparam name="TId"><see cref="IStrongId"/> type</typeparam>
	public static TId RndId<TId>()
		where TId : StrongId<long>, new() =>
		new() { Value = Rnd.Lng };
}
