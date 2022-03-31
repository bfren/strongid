// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace StrongId.Functions;

public static partial class TypeF
{
	/// <summary>
	/// Cache value types as they are looked up a lot
	/// </summary>
	internal static IMemoryCache StrongIdValueTypes { get; } = new MemoryCache(new MemoryCacheOptions());

	/// <summary>
	/// Get the <see cref="IStrongId{T}"/> Value type if <paramref name="type"/>
	/// implements <see cref="IStrongId{T}"/>
	/// </summary>
	/// <param name="type">Type to check</param>
	public static Type? GetStrongIdValueType(Type type) =>
		StrongIdValueTypes.GetOrCreate(
			type.FullName ?? type.Name,
			_ =>
			{
				// Strong IDs must implement IStrongId as a minimum
				if (!typeof(IStrongId).IsAssignableFrom(type))
				{
					return null;
				}

				// Get    .. all interfaces implemented by the type we are checking
				// If     .. the interface is a generic type (i.e. has generic type arguments)
				//        .. and the generic type definition is IStrongId<>
				// Select .. the first (and only) generic type argument - this is the IStrongId<T> Value type
				var valueTypesQuery = from i in type.GetInterfaces()
									  where i.IsGenericType
									  && i.GetGenericTypeDefinition() == typeof(IStrongId<>)
									  select i.GenericTypeArguments[0];
				var valueTypes = valueTypesQuery.ToList();

				// This means the type doesn't implement IStrongId<T>,
				// or it implements it with multiple Value types, which is not supported
				if (valueTypes.Count != 1)
				{
					return null;
				}

				// Precisely one value type has been identified, so return it
				return valueTypes[0];
			}
		);
}
