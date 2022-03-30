// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId.Dapper.StrongIdTypeHandler_Tests;

public class Parse_Tests
{
	public static IEnumerable<object[]> Null_Or_Empty_Or_Invalid_Data()
	{
		yield return new object[] { null! };
		yield return new object[] { "" };
		yield return new object[] { " " };
		yield return new object[] { true };
		yield return new object[] { "something wrong here" };
	}

	[Theory]
	[MemberData(nameof(Null_Or_Empty_Or_Invalid_Data))]
	public void Null_Or_Empty_Or_Invalid_Input__Returns_Default_GuidId(object input)
	{
		// Arrange
		var handler = new StrongIdTypeHandler<TestGuidId>();

		// Act
		var result = handler.Parse(input);

		// Assert
		Assert.Equal(Guid.Empty, result.Value);
	}

	[Theory]
	[MemberData(nameof(Null_Or_Empty_Or_Invalid_Data))]
	public void Null_Or_Empty_Or_Invalid_Input__Returns_Default_IntId(object input)
	{
		// Arrange
		var handler = new StrongIdTypeHandler<TestIntId>();

		// Act
		var result = handler.Parse(input);

		// Assert
		Assert.Equal(0, result.Value);
	}

	[Theory]
	[MemberData(nameof(Null_Or_Empty_Or_Invalid_Data))]
	public void Null_Or_Empty_Or_Invalid_Input__Returns_Default_LongId(object input)
	{
		// Arrange
		var handler = new StrongIdTypeHandler<TestLongId>();

		// Act
		var result = handler.Parse(input);

		// Assert
		Assert.Equal(0L, result.Value);
	}

	[Theory]
	[InlineData("5da27e31-6f74-4256-a654-1075641324fe")]
	[InlineData(" 56f1fcf2-1fa4-4da4-9fc7-636afaf017f7 ")]
	[InlineData("582f6388075c4092a02e92842d9165a1")]
	public void Valid_Input__Returns_GuidId(object input)
	{
		// Arrange
		var handler = new StrongIdTypeHandler<TestGuidId>();
		var expected = Guid.Parse(input.ToString()!);

		// Act
		var result = handler.Parse(input);

		// Assert
		Assert.Equal(expected, result.Value);
	}

	[Theory]
	[InlineData(42, 42)]
	[InlineData("42", 42)]
	[InlineData(" 42 ", 42)]
	public void Valid_Input__Returns_IntId(object input, int expected)
	{
		// Arrange
		var handler = new StrongIdTypeHandler<TestIntId>();

		// Act
		var result = handler.Parse(input);

		// Assert
		Assert.Equal(expected, result.Value);
	}

	[Theory]
	[InlineData(42, 42L)]
	[InlineData(42L, 42L)]
	[InlineData(42.0, 42L)]
	[InlineData("42", 42L)]
	[InlineData(" 42 ", 42L)]
	public void Valid_Input__Returns_LongId(object input, long expected)
	{
		// Arrange
		var handler = new StrongIdTypeHandler<TestLongId>();

		// Act
		var result = handler.Parse(input);

		// Assert
		Assert.Equal(expected, result.Value);
	}

	[Fact]
	public void Invalid_Value_Type__Throws_InvalidOperationException()
	{
		// Arrange
		var handler = new StrongIdTypeHandler<InvalidDateTimeId>();

		// Act
		var action = () => handler.Parse(Rnd.DateTime);

		// Assert
		Assert.Throws<InvalidOperationException>(action);
	}

	public sealed record class TestGuidId : GuidId;

	public sealed record class TestIntId : IntId;

	public sealed record class TestLongId : LongId;

	public sealed record class InvalidObjectId(object Value) : IStrongId
	{
		public InvalidObjectId() : this(new()) { }
	}

	public sealed record class InvalidDateTimeId(DateTime Value) : IStrongId<DateTime>
	{
		public InvalidDateTimeId() : this(DateTime.MinValue) { }
	}
}
