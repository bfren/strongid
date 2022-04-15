// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StrongId.Mvc.ModelBinding.StrongIdModelBinder_Tests;

public class BindModelAsync_Tests
{
	internal static (StrongIdModelBinder<TestLongId, long>, Vars) Setup()
	{
		var modelName = Rnd.Str;
		var modelState = new ModelStateDictionary();
		var modelValue = Rnd.Str;
		var valueResult = new ValueProviderResult(modelValue);

		var valueProvider = Substitute.For<IValueProvider>();
		valueProvider.GetValue(modelName)
			.Returns(valueResult);

		var bindingResult = new ModelBindingResult();

		var bindingContext = Substitute.ForPartsOf<ModelBindingContext>();
		bindingContext.ModelName
			.Returns(modelName);
		bindingContext.Result
			.Returns(bindingResult);
		bindingContext.ModelState
			.Returns(modelState);
		bindingContext.ValueProvider
			.Returns(valueProvider);

		var parse = Substitute.For<Func<string?, Maybe<long>>>();
		parse.Invoke(default)
			.ReturnsForAnyArgs(Create.None<long>());

		var binder = Substitute.For<StrongIdModelBinder<TestLongId, long>>();
		binder.Parse(default)
			.ReturnsForAnyArgs(x => parse(x[0].ToString()));

		return (binder, new(bindingContext, bindingResult, modelName, modelState, modelValue, parse, valueProvider));
	}

	public sealed record class Vars(
		ModelBindingContext BindingContext,
		ModelBindingResult BindingResult,
		string ModelName,
		ModelStateDictionary ModelState,
		string ModelValue,
		Func<string?, Maybe<long>> Parse,
		IValueProvider ValueProvider
	);

	[Fact]
	public async Task ValueProvider_Result_Is_None__Does_Not_Change_BindingResult()
	{
		// Arrange
		var (binder, v) = Setup();
		v.ValueProvider.GetValue(v.ModelName)
			.Returns(ValueProviderResult.None);

		// Act
		await binder.BindModelAsync(v.BindingContext);

		// Assert
		Assert.Equal(v.BindingResult, v.BindingContext.Result);
	}

	[Fact]
	public async Task ValueProvider_Result_Is_Not_None__Sets_Model_Value_Using_Result_Value()
	{
		// Arrange
		var (binder, v) = Setup();

		// Act
		await binder.BindModelAsync(v.BindingContext);

		// Assert
		Assert.Equal(v.ModelValue, v.ModelState[v.ModelName]?.AttemptedValue);
	}

	[Fact]
	public async Task ValueProvider_Result_Is_Not_None__Calls_Parse__With_Correct_Value()
	{
		// Arrange
		var (binder, v) = Setup();

		// Act
		await binder.BindModelAsync(v.BindingContext);

		// Assert
		v.Parse.Received().Invoke(v.ModelValue);
	}

	[Fact]
	public async Task ValueProvider_Result_Is_Not_None__Calls_Parse__Receives_None__Returns_Result_Failed()
	{
		// Arrange
		var (binder, v) = Setup();

		// Act
		await binder.BindModelAsync(v.BindingContext);

		// Assert
		Assert.False(v.BindingContext.Result.IsModelSet);
		Assert.Equal("Failed", v.BindingContext.Result.ToString());
	}

	[Fact]
	public async Task ValueProvider_Result_Is_Not_None__Calls_Parse__Receives_Some__Returns_Result_Success_With_Value()
	{
		// Arrange
		var (binder, v) = Setup();
		var id = Rnd.Lng;
		v.Parse(v.ModelValue)
			.Returns(id);

		// Act
		await binder.BindModelAsync(v.BindingContext);

		// Assert
		Assert.True(v.BindingContext.Result.IsModelSet);
		var model = Assert.IsType<TestLongId>(v.BindingContext.Result.Model);
		Assert.Equal(id, model.Value);
	}

	public sealed record class TestLongId : LongId;
}
