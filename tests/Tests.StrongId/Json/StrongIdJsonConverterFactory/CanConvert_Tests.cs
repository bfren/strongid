// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId.Json.StrongIdJsonConverterFactory_Tests;

public class CanConvert_Tests
{
	[Fact]
	public void Type_Implements_IStrongId__Returns_True()
	{
		// Arrange
		var factory = new StrongIdJsonConverterFactory();
		var type = typeof(TestId);

		// Act
		var result = factory.CanConvert(type);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void Type_Does_Not_Implement_IStrongId__Returns_False()
	{
		// Arrange
		var factory = new StrongIdJsonConverterFactory();
		var type = typeof(RandomClass);

		// Act
		var result = factory.CanConvert(type);

		// Assert
		Assert.False(result);
	}

	public sealed record class TestId : GuidId;

	public sealed record class RandomClass;
}
