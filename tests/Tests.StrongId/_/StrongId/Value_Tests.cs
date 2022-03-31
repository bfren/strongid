// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId.StrongId_Tests;

public class Value_Tests
{
	public object Get<T>(T id)
		where T : IStrongId =>
		id.Value;

	public IStrongId Set<T>(object value)
		where T : IStrongId, new() =>
		new T { Value = value };

	[Fact]
	public void Generic_Get__With_Value__Returns_Value()
	{
		// Arrange
		var value = Rnd.Guid;
		var id = new TestId { Value = value };

		// Act
		var result = Get(id);

		// Assert
		Assert.Equal(value, result);
	}

	[Fact]
	public void Generic_Get__With_Null_Value__Returns_New_Object()
	{
		// Arrange
		var id = new NullableTestId(null);

		// Act
		var result = Get(id);

		// Assert
		Assert.NotNull(result);
	}

	[Fact]
	public void Generic_Set__Receives_Correct_Type__Uses_Value()
	{
		// Arrange
		var input = Rnd.Guid;

		// Act
		var result = Set<TestId>(input);

		// Assert
		Assert.Equal(input, result.Value);
	}

	[Fact]
	public void Generic_Set__Receives_Incorrect_Type__Throws_InvalidCastException()
	{
		// Arrange
		var value = Rnd.Str;

		// Act
		var action = void () => Set<TestId>(value);

		// Assert
		var ex = Assert.Throws<InvalidCastException>(action);
		Assert.Equal($"Unable to set ID value to {value}: expecting an object of type {typeof(Guid)}.", ex.Message);
	}

	public sealed record class NullableTestId(string? Value) : IStrongId<string?>;

	public sealed record class TestId : GuidId;
}
