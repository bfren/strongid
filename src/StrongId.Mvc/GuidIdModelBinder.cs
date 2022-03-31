// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System;

namespace StrongId.Mvc;

/// <summary>
/// <see cref="IStrongId"/> MVC model binder for <see cref="Guid"/> value types
/// </summary>
/// <typeparam name="TId"><see cref="IStrongId"/> type</typeparam>
public sealed class GuidIdModelBinder<TId> : StrongIdModelBinder<TId, Guid>
	where TId : GuidId, new()
{
	/// <inheritdoc/>
	internal override Maybe<Guid> Parse(string? input) =>
		F.ParseGuid(input);
}
