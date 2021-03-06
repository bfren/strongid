// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId.Json.StrongIdJsonConverterFactory_Tests;

public class CreateConverter_Tests
{
	[Fact]
	public void Type_Does_Not_Implement_IStrongId__Throws_JsonConverterException()
	{
		// Arrange
		var factory = new StrongIdJsonConverterFactory();
		var type = typeof(RandomClass);

		// Act
		var action = void () => factory.CreateConverter(type, Helpers.Options);

		// Assert
		var ex = Assert.Throws<JsonConverterException>(action);
		Assert.Equal(
			$"{type} is an invalid {typeof(IStrongId)}: " +
			"please implement one of the provided abstract ID record types.",
			ex.Message
		);
	}

	[Fact]
	public void Type_Does_Not_Have_Parameterless_Constructor__Throws_JsonConverterException()
	{
		// Arrange
		var factory = new StrongIdJsonConverterFactory();
		var type = typeof(TestIdWithoutParameterlessConstructor);

		// Act
		var action = void () => factory.CreateConverter(type, Helpers.Options);

		// Assert
		var ex = Assert.Throws<JsonConverterException>(action);
		Assert.Equal(
			$"{type} does not have a parameterless constructor.",
			ex.Message
		);
	}

	[Fact]
	public void Type_Implements_IStrongId__With_Unsupported_Value_Type__Throws_JsonConverterException()
	{
		// Arrange
		var factory = new StrongIdJsonConverterFactory();
		var type = typeof(TestIdWithInvalidType);

		// Act
		var action = void () => factory.CreateConverter(type, Helpers.Options);

		// Assert
		var ex = Assert.Throws<JsonConverterException>(action);
		Assert.Equal($"StrongId with value type {typeof(DateTime)} is not supported.", ex.Message);
	}

	[Fact]
	public void Type_Implements_IStrongId__With_Supported_Value_Type__Returns_Correct_Converter()
	{
		// Arrange
		var factory = new StrongIdJsonConverterFactory();
		var type = typeof(TestId);

		// Act
		var result = factory.CreateConverter(type, Helpers.Options);

		// Assert
		Assert.IsType<GuidIdJsonConverter<TestId>>(result);
	}

	public sealed record class RandomClass;

	public sealed record class TestIdWithoutParameterlessConstructor(long Value) : LongId(Value);

	public sealed record class TestIdWithInvalidType(DateTime Value) : StrongId<DateTime>(Value)
	{
		public TestIdWithInvalidType() : this(Rnd.DateTime) { }
	}

	public sealed record class TestId : GuidId;
}
