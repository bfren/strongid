// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId.Mvc.ModelBinding;

/// <summary>
/// <see cref="IStrongId"/> MVC model binder for <see cref="ulong"/> value types
/// </summary>
/// <typeparam name="TId"><see cref="IStrongId"/> type</typeparam>
public sealed class ULongIdModelBinder<TId> : StrongIdModelBinder<TId, ulong>
	where TId : ULongId, new()
{
	/// <inheritdoc/>
	internal override ulong Default =>
		0UL;

	/// <inheritdoc/>
	internal override Maybe<ulong> Parse(string? input) =>
		F.ParseUInt64(input);
}
