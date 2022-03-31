// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StrongId.Functions;

namespace StrongId.Mvc;

/// <summary>
/// <see cref="IStrongId"/> MVC model binder provider
/// </summary>
public sealed class StrongIdModelBinderProvider : IModelBinderProvider
{
	/// <summary>
	/// If the model type implements <see cref="IStrongId"/>, create <see cref="StrongIdModelBinder{TId, TIdValue}"/>
	/// </summary>
	/// <param name="context">ModelBinderProviderContext</param>
	public IModelBinder? GetBinder(ModelBinderProviderContext context) =>
		GetBinderFromModelType(context.Metadata.ModelType);

	/// <summary>
	/// Get binder from the specified model type
	/// </summary>
	/// <param name="modelType">Model Type</param>
	/// <exception cref="ModelBinderException"></exception>
	internal static IModelBinder? GetBinderFromModelType(Type modelType)
	{
		// If this type isn't a StrongId, return null so MVC can move on to try the next model binder
		var strongIdValueType = TypeF.GetStrongIdValueType(modelType);
		if (strongIdValueType is null)
		{
			return null;
		}

		// Use the Value type to determine which binder to use
		var strongIdBinder = strongIdValueType switch
		{
			Type t when t == typeof(Guid) =>
				typeof(GuidIdModelBinder<>),

			Type t when t == typeof(int) =>
				typeof(IntIdModelBinder<>),

			Type t when t == typeof(long) =>
				typeof(LongIdModelBinder<>),

			{ } t =>
				throw new ModelBinderException($"StrongId with value type {t} is not supported.")
		};

		// Attempt to create and return the binder
		var genericType = strongIdBinder.MakeGenericType(modelType);
		return Activator.CreateInstance(genericType) switch
		{
			IModelBinder x =>
				x,

			_ =>
				throw new ModelBinderException($"Unable to create {typeof(StrongIdModelBinder<,>)} for type {modelType}.")
		};
	}
}
