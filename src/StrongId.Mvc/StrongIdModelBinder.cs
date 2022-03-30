// Mileage Tracker
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StrongId.Mvc;

/// <summary>
/// Binds an ID to a StrongID
/// </summary>
/// <typeparam name="T">StrongID type</typeparam>
public sealed class StrongIdModelBinder<T> : IModelBinder
	where T : IStrongId, new()
{
	/// <summary>
	/// Get value and attempt to parse as a long
	/// </summary>
	/// <param name="bindingContext">ModelBindingContext</param>
	public Task BindModelAsync(ModelBindingContext bindingContext)
	{
		// Get the value from the context
		var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
		if (valueProviderResult == ValueProviderResult.None)
		{
			return Task.CompletedTask;
		}

		bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

		// Get the value and attempt to parse it as a long
		bindingContext.Result = F.ParseInt64(valueProviderResult.FirstValue).Switch(
			some: x => ModelBindingResult.Success(new T { Value = x }),
			none: _ => ModelBindingResult.Failed()
		);

		return Task.CompletedTask;
	}
}