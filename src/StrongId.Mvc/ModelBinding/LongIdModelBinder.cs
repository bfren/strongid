// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId.Mvc.ModelBinding;

/// <summary>
/// <see cref="IStrongId"/> MVC model binder for <see cref="long"/> value types
/// </summary>
/// <typeparam name="TId"><see cref="IStrongId"/> type</typeparam>
public sealed class LongIdModelBinder<TId> : StrongIdModelBinder<TId, long>
	where TId : LongId, new()
{
	/// <inheritdoc/>
	internal override Maybe<long> Parse(string? input) =>
		F.ParseInt64(input);
}
