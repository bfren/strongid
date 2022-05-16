// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using RndF;

namespace StrongId.Testing;

/// <summary>
/// Generate StrongIds with random values
/// </summary>
public static class Generator
{
	/// <summary>
	/// Generate a new <typeparamref name="TId"/> with a random <see cref="IStrongId.Value"/>
	/// </summary>
	/// <typeparam name="TId"><see cref="GuidId"/> type</typeparam>
	public static TId GuidId<TId>()
		where TId : GuidId, new() =>
		new() { Value = Rnd.Guid };

	/// <summary>
	/// Generate a new <typeparamref name="TId"/> with a random <see cref="IStrongId.Value"/>
	/// </summary>
	/// <typeparam name="TId"><see cref="IntId"/> type</typeparam>
	/// <param name="limit">If true, ID Value will be limited to 0-10000 - for testing this is all that's needed</param>
	public static TId IntId<TId>(bool limit = true)
		where TId : IntId, new() =>
		new() { Value = limit ? Rnd.Int : Rnd.NumberF.GetInt32() };

	/// <summary>
	/// Generate a new <typeparamref name="TId"/> with a random <see cref="IStrongId.Value"/>
	/// </summary>
	/// <typeparam name="TId"><see cref="LongId"/> type</typeparam>
	/// <param name="limit">If true, ID Value will be limited to 0-10000 - for testing this is all that's needed</param>
	public static TId LongId<TId>(bool limit = true)
		where TId : LongId, new() =>
		new() { Value = limit ? Rnd.Lng : Rnd.NumberF.GetInt64() };

	/// <summary>
	/// Generate a new <typeparamref name="TId"/> with a random <see cref="IStrongId.Value"/>
	/// </summary>
	/// <typeparam name="TId"><see cref="UIntId"/> type</typeparam>
	/// <param name="limit">If true, ID Value will be limited to 0-10000 - for testing this is all that's needed</param>
	public static TId UIntId<TId>(bool limit = true)
		where TId : UIntId, new() =>
		new() { Value = limit ? Rnd.UInt : Rnd.NumberF.GetUInt32() };

	/// <summary>
	/// Generate a new <typeparamref name="TId"/> with a random <see cref="IStrongId.Value"/>
	/// </summary>
	/// <typeparam name="TId"><see cref="ULongId"/> type</typeparam>
	/// <param name="limit">If true, ID Value will be limited to 0-10000 - for testing this is all that's needed</param>
	public static TId ULongId<TId>(bool limit = true)
		where TId : ULongId, new() =>
		new() { Value = limit ? Rnd.ULng : Rnd.NumberF.GetUInt64() };
}
