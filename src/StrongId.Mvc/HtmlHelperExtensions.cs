// StrongId: Strongly-Typed ID Values
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StrongId.Mvc;

/// <summary>
/// HtmlHelper extensions
/// </summary>
public static class HtmlHelperExtensions
{
	/// <summary>
	/// Output a hidden HTML input for an ID value
	/// </summary>
	/// <typeparam name="TModel">Model type</typeparam>
	/// <typeparam name="TId">ID type</typeparam>
	/// <param name="this"></param>
	/// <param name="expression"></param>
	public static IHtmlContent HiddenForId<TModel, TId>(this IHtmlHelper<TModel> @this, Expression<Func<TModel, TId?>> expression)
		where TId : IStrongId
	{
		// Use helper to generate input name
		var name = @this.NameFor(expression);

		// Get nullable value
		var value = @this.ViewData.Model switch
		{
			TModel model =>
				expression.Compile().Invoke(model)?.Value,

			_ =>
				null
		};

		// Return hidden input
		return @this.Hidden(name, value);
	}
}
