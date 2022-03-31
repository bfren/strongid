// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId;

/// <inheritdoc cref="IStrongId{T}"/>
/// <param name="Value">ID Value</param>
public abstract record class StrongId<T>(T Value) : IStrongId<T>;
