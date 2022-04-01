// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId;

/// <summary>
/// Implementation using <see cref="int"/> as the Value type
/// </summary>
public abstract record class IntId : StrongId<int>
{
	/// <summary>
	/// Create ID with default value
	/// </summary>
	protected IntId() : base(0) { }

	/// <summary>
	/// Create ID with value
	/// </summary>
	/// <param name="value">ID Value</param>
	protected IntId(int value) : base(value) { }
}
