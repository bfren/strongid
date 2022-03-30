// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System;
using System.Data;
using StrongId.Functions;

namespace StrongId.Dapper;

/// <summary>
/// <see cref="IStrongId"/> TypeHandler
/// </summary>
/// <typeparam name="T"><see cref="IStrongId"/> type</typeparam>
public sealed class StrongIdTypeHandler<T> : global::Dapper.SqlMapper.TypeHandler<T>
	where T : class, IStrongId, new()
{
	/// <summary>
	/// Parse value and create new <see cref="IStrongId"/>
	/// </summary>
	/// <param name="value"><see cref="IStrongId"/> Value</param>
	/// <exception cref="InvalidOperationException"></exception>
	public override T Parse(object value) =>
		new()
		{
			Value = TypeF.GetStrongIdValueType(typeof(T)) switch
			{
				Type t when t == typeof(Guid) =>
					GetValueAsType(value, F.ParseGuid, Guid.Empty),

				Type t when t == typeof(int) =>
					GetValueAsType(value, F.ParseInt32, 0),

				Type t when t == typeof(long) =>
					GetValueAsType(value, F.ParseInt64, 0L),

				_ =>
					throw new InvalidOperationException($"StrongId with value type {typeof(object)} is not supported.")
			}
		};

	/// <summary>
	/// Set ID value
	/// </summary>
	/// <param name="parameter">IDbDataParameter</param>
	/// <param name="value"><see cref="IStrongId"/> value</param>
	public override void SetValue(IDbDataParameter parameter, T value) =>
		parameter.Value = value.Value;

	/// <summary>
	/// Returns a strongly-typed value
	/// </summary>
	/// <typeparam name="TIdValue">StrongId Value type</typeparam>
	/// <param name="value">Value to handle</param>
	/// <param name="parse">Parse function</param>
	/// <param name="defaultValue">Default value if parsing fails</param>
	internal TIdValue GetValueAsType<TIdValue>(object value, Func<string?, Maybe<TIdValue>> parse, TIdValue defaultValue) =>
		value switch
		{
			TIdValue id =>
				id,

			_ =>
				parse(value?.ToString()).Unwrap(defaultValue)
		};
}
