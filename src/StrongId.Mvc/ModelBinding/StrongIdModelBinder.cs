// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StrongId.Mvc.ModelBinding;

/// <summary>
/// <see cref="IStrongId"/> MVC model binder
/// </summary>
/// <typeparam name="TId"><see cref="IStrongId"/> type</typeparam>
/// <typeparam name="TIdValue"><see cref="IStrongId"/> Value type</typeparam>
public abstract class StrongIdModelBinder<TId, TIdValue> : IModelBinder
	where TId : StrongId<TIdValue>, new()
{
	/// <summary>
	/// StrongId Value parse function
	/// </summary>
	/// <param name="input">Input string from the model binder</param>
	internal abstract Maybe<TIdValue> Parse(string? input);

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

		// Set the model state value
		bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

		// Get the actual value and attempt to parse it
		bindingContext.Result =
			Parse(
				valueProviderResult.FirstValue
			)
			.Switch(
				some: x => ModelBindingResult.Success(new TId { Value = x }),
				none: _ => ModelBindingResult.Failed()
			);

		return Task.CompletedTask;
	}
}
