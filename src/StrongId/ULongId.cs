// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId;

/// <summary>
/// Implementation using <see cref="ulong"/> as the Value type
/// </summary>
public abstract record class ULongId : StrongId<ulong>
{
	/// <summary>
	/// Create ID with default value
	/// </summary>
	protected ULongId() : base(0UL) { }

	/// <summary>
	/// Create ID with value
	/// </summary>
	/// <param name="value">ID Value</param>
	protected ULongId(ulong value) : base(value) { }
}
