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

				Type t when t == typeof(uint) =>
					GetValueAsType(value, F.ParseUInt32, 0u),

				Type t when t == typeof(ulong) =>
					GetValueAsType(value, F.ParseUInt64, 0UL),

				Type t =>
					throw new InvalidOperationException($"StrongId with value type {t} is not supported."),

				_ =>
					throw new InvalidOperationException($"StrongId with value type {typeof(object)} is not supported.")
			}
		};

	/// <summary>
	/// Set ID value
	/// </summary>
	/// <param name="parameter"></param>
	/// <param name="value"><see cref="IStrongId"/> value</param>
	/// <exception cref="ArgumentNullException"></exception>
	/// <exception cref="InvalidOperationException"></exception>
	public override void SetValue(IDbDataParameter parameter, T value)
	{
		// Shouldn't happen because Dapper won't pass a null parameter, but best to check anyway
		if (parameter is null)
		{
			return;
		}

		// Set DbType according to the ID value type rather than the StrongId type
		parameter.DbType = TypeF.GetStrongIdValueType(typeof(T)) switch
		{
			Type t when t == typeof(Guid) =>
				DbType.Guid,

			Type t when t == typeof(int) =>
				DbType.Int32,

			Type t when t == typeof(long) =>
				DbType.Int64,

			Type t when t == typeof(uint) =>
				DbType.UInt32,

			Type t when t == typeof(ulong) =>
				DbType.UInt64,

			{ } t =>
				throw new InvalidOperationException($"StrongId with value type {t} is not supported."),

			_ =>
				throw new InvalidOperationException($"{typeof(T)} is not a valid StrongId.")
		};

		// If the value is null, use default ID value
		parameter.Value = value switch
		{
			T id =>
				id.Value,

			_ =>
				new T().Value
		};
	}

	/// <summary>
	/// Returns a strongly-typed value
	/// </summary>
	/// <typeparam name="TIdValue"><see cref="IStrongId"/> Value type</typeparam>
	/// <param name="value">Value to parse</param>
	/// <param name="parse">Parse function</param>
	/// <param name="defaultValue">Default value to be used if parsing fails</param>
	internal TIdValue GetValueAsType<TIdValue>(object value, Func<string?, Maybe<TIdValue>> parse, TIdValue defaultValue) =>
		value switch
		{
			TIdValue id =>
				id,

			_ =>
				parse(value?.ToString()).Unwrap(defaultValue)
		};
}
