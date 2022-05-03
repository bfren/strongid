// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StrongId.Mvc.ModelBinding.StrongIdModelBinder_Tests;

public class BindModelAsync_Tests
{
	internal static (StrongIdModelBinder<TId, TIdValue>, Vars<TIdValue>) Setup<TId, TIdValue>()
		where TId : StrongId<TIdValue>, new()
	{
		var modelName = Rnd.Str;
		var modelState = new ModelStateDictionary();
		var modelValue = Rnd.Str;
		var valueResult = new ValueProviderResult(modelValue);

		var valueProvider = Substitute.For<IValueProvider>();
		valueProvider.GetValue(modelName).Returns(valueResult);

		var bindingResult = new ModelBindingResult();

		var bindingContext = Substitute.ForPartsOf<ModelBindingContext>();
		bindingContext.ModelName.Returns(modelName);
		bindingContext.Result.Returns(bindingResult);
		bindingContext.ModelState.Returns(modelState);
		bindingContext.ValueProvider.Returns(valueProvider);

		var parse = Substitute.For<Func<string?, Maybe<TIdValue>>>();
		parse.Invoke(default).ReturnsForAnyArgs(Create.None<TIdValue>());

		var binder = Substitute.For<StrongIdModelBinder<TId, TIdValue>>();
		binder.Parse(default).ReturnsForAnyArgs(x => parse(x[0].ToString()));

		return (binder, new(bindingContext, bindingResult, modelName, modelState, modelValue, parse, valueProvider));
	}

	public sealed record class Vars<TIdValue>(
		ModelBindingContext BindingContext,
		ModelBindingResult BindingResult,
		string ModelName,
		ModelStateDictionary ModelState,
		string ModelValue,
		Func<string?, Maybe<TIdValue>> Parse,
		IValueProvider ValueProvider
	);

	private async Task ValueProvider_Result_Is_None__Does_Not_Change_BindingResult<TId, TIdValue>()
		where TId : StrongId<TIdValue>, new()
	{
		// Arrange
		var (binder, v) = Setup<TId, TIdValue>();
		v.ValueProvider.GetValue(v.ModelName)
			.Returns(ValueProviderResult.None);

		// Act
		await binder.BindModelAsync(v.BindingContext);

		// Assert
		Assert.Equal(v.BindingResult, v.BindingContext.Result);
	}

	[Fact]
	public async Task Guid__ValueProvider_Result_Is_None__Does_Not_Change_BindingResult() =>
		await ValueProvider_Result_Is_None__Does_Not_Change_BindingResult<TestGuidId, Guid>();

	[Fact]
	public async Task Int__ValueProvider_Result_Is_None__Does_Not_Change_BindingResult() =>
		await ValueProvider_Result_Is_None__Does_Not_Change_BindingResult<TestIntId, int>();

	[Fact]
	public async Task Long__ValueProvider_Result_Is_None__Does_Not_Change_BindingResult() =>
		await ValueProvider_Result_Is_None__Does_Not_Change_BindingResult<TestLongId, long>();

	private async Task ValueProvider_Result_Is_Not_None__Sets_Model_Value_Using_Result_Value<TId, TIdValue>()
		where TId : StrongId<TIdValue>, new()
	{
		// Arrange
		var (binder, v) = Setup<TId, TIdValue>();

		// Act
		await binder.BindModelAsync(v.BindingContext);

		// Assert
		Assert.Equal(v.ModelValue, v.ModelState[v.ModelName]?.AttemptedValue);
	}

	[Fact]
	public async Task Guid__ValueProvider_Result_Is_Not_None__Sets_Model_Value_Using_Result_Value() =>
		await ValueProvider_Result_Is_Not_None__Sets_Model_Value_Using_Result_Value<TestGuidId, Guid>();

	[Fact]
	public async Task Int__ValueProvider_Result_Is_Not_None__Sets_Model_Value_Using_Result_Value() =>
		await ValueProvider_Result_Is_Not_None__Sets_Model_Value_Using_Result_Value<TestIntId, int>();

	[Fact]
	public async Task Long__ValueProvider_Result_Is_Not_None__Sets_Model_Value_Using_Result_Value() =>
		await ValueProvider_Result_Is_Not_None__Sets_Model_Value_Using_Result_Value<TestLongId, long>();

	private async Task ValueProvider_Result_Is_Not_None__Calls_Parse__With_Correct_Value<TId, TIdValue>()
		where TId : StrongId<TIdValue>, new()
	{
		// Arrange
		var (binder, v) = Setup<TId, TIdValue>();

		// Act
		await binder.BindModelAsync(v.BindingContext);

		// Assert
		v.Parse.Received().Invoke(v.ModelValue);
	}

	[Fact]
	public async Task Guid__ValueProvider_Result_Is_Not_None__Calls_Parse__With_Correct_Value() =>
		await ValueProvider_Result_Is_Not_None__Calls_Parse__With_Correct_Value<TestGuidId, Guid>();

	[Fact]
	public async Task Int__ValueProvider_Result_Is_Not_None__Calls_Parse__With_Correct_Value() =>
		await ValueProvider_Result_Is_Not_None__Calls_Parse__With_Correct_Value<TestIntId, int>();

	[Fact]
	public async Task Long__ValueProvider_Result_Is_Not_None__Calls_Parse__With_Correct_Value() =>
		await ValueProvider_Result_Is_Not_None__Calls_Parse__With_Correct_Value<TestLongId, long>();

	private async Task ValueProvider_Result_Is_Not_None__Calls_Parse__Receives_None__Returns_Result_Success_With_Default<TId, TIdValue>()
		where TId : StrongId<TIdValue>, new()
	{
		// Arrange
		var (binder, v) = Setup<TId, TIdValue>();

		// Act
		await binder.BindModelAsync(v.BindingContext);

		// Assert
		Assert.True(v.BindingContext.Result.IsModelSet);
		var model = Assert.IsType<TId>(v.BindingContext.Result.Model);
		Assert.Equal(binder.Default, model.Value);
	}

	[Fact]
	public async Task Guid__ValueProvider_Result_Is_Not_None__Calls_Parse__Receives_None__Returns_Result_Success_With_Default() =>
		await ValueProvider_Result_Is_Not_None__Calls_Parse__Receives_None__Returns_Result_Success_With_Default<TestGuidId, Guid>();

	[Fact]
	public async Task Int__ValueProvider_Result_Is_Not_None__Calls_Parse__Receives_None__Returns_Result_Success_With_Default() =>
		await ValueProvider_Result_Is_Not_None__Calls_Parse__Receives_None__Returns_Result_Success_With_Default<TestIntId, int>();

	[Fact]
	public async Task Long__ValueProvider_Result_Is_Not_None__Calls_Parse__Receives_None__Returns_Result_Success_With_Default() =>
		await ValueProvider_Result_Is_Not_None__Calls_Parse__Receives_None__Returns_Result_Success_With_Default<TestLongId, long>();

	private async Task ValueProvider_Result_Is_Not_None__Calls_Parse__Receives_Some__Returns_Result_Success_With_Value<TId, TIdValue>(TIdValue id)
		where TId : StrongId<TIdValue>, new()
	{
		// Arrange
		var (binder, v) = Setup<TId, TIdValue>();
		v.Parse(v.ModelValue)
			.Returns(id);

		// Act
		await binder.BindModelAsync(v.BindingContext);

		// Assert
		Assert.True(v.BindingContext.Result.IsModelSet);
		var model = Assert.IsType<TId>(v.BindingContext.Result.Model);
		Assert.Equal(id, model.Value);
	}

	[Fact]
	public async Task Guid__ValueProvider_Result_Is_Not_None__Calls_Parse__Receives_Some__Returns_Result_Success_With_Value() =>
		await ValueProvider_Result_Is_Not_None__Calls_Parse__Receives_Some__Returns_Result_Success_With_Value<TestGuidId, Guid>(Rnd.Guid);

	[Fact]
	public async Task Int__ValueProvider_Result_Is_Not_None__Calls_Parse__Receives_Some__Returns_Result_Success_With_Value() =>
		await ValueProvider_Result_Is_Not_None__Calls_Parse__Receives_Some__Returns_Result_Success_With_Value<TestIntId, int>(Rnd.Int);

	[Fact]
	public async Task Long__ValueProvider_Result_Is_Not_None__Calls_Parse__Receives_Some__Returns_Result_Success_With_Value() =>
		await ValueProvider_Result_Is_Not_None__Calls_Parse__Receives_Some__Returns_Result_Success_With_Value<TestLongId, long>(Rnd.Lng);

	public sealed record class TestGuidId : GuidId;

	public sealed record class TestIntId : IntId;

	public sealed record class TestLongId : LongId;
}
