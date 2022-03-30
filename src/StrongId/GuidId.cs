// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System;
using RndF;

namespace StrongId;

/// <summary>
/// Implementation using <see cref="Guid"/> as the Value type
/// </summary>
public abstract record class GuidId : StrongId<Guid>
{
	/// <summary>
	/// Generate a new <typeparamref name="TId"/> with a random <see cref="IStrongId.Value"/>
	/// </summary>
	/// <typeparam name="TId"><see cref="IStrongId"/> type</typeparam>
	public static TId RndId<TId>()
		where TId : StrongId<Guid>, new() =>
		new() { Value = Rnd.Guid };
}
