// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using static StrongId.Testing.Generator;

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
		var id = GuidId<TestGuidId>();

		// Act
		var result = Get(id);

		// Assert
		Assert.Equal(id.Value, result);
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
		var result = Set<TestGuidId>(input);

		// Assert
		Assert.Equal(input, result.Value);
	}

	[Fact]
	public void Generic_Set__Receives_Incorrect_Type__Throws_InvalidCastException()
	{
		// Arrange
		var value = Rnd.Str;

		// Act
		var action = void () => Set<TestGuidId>(value);

		// Assert
		var ex = Assert.Throws<InvalidCastException>(action);
		Assert.Equal($"Unable to set ID value to {value}: expecting an object of type {typeof(Guid)}.", ex.Message);
	}

	public sealed record class NullableTestId(string? Value) : IStrongId<string?>;

	public sealed record class TestGuidId : GuidId;
}
