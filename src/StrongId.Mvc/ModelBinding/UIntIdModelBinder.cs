// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId.Mvc.ModelBinding;

/// <summary>
/// <see cref="IStrongId"/> MVC model binder for <see cref="uint"/> value types
/// </summary>
/// <typeparam name="TId"><see cref="IStrongId"/> type</typeparam>
public sealed class UIntIdModelBinder<TId> : StrongIdModelBinder<TId, uint>
	where TId : UIntId, new()
{
	/// <inheritdoc/>
	internal override uint Default =>
		0u;

	/// <inheritdoc/>
	internal override Maybe<uint> Parse(string? input) =>
		F.ParseUInt32(input);
}
