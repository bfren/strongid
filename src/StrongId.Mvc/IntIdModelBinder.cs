// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId.Mvc;

/// <summary>
/// <see cref="IStrongId"/> MVC model binder for <see cref="int"/> value types
/// </summary>
/// <typeparam name="TId"><see cref="IStrongId"/> type</typeparam>
public sealed class IntIdModelBinder<TId> : StrongIdModelBinder<TId, int>
	where TId : class, IStrongId<int>, new()
{
	/// <inheritdoc/>
	internal override Maybe<int> Parse(string? input) =>
		F.ParseInt32(input);
}
