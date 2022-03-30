// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId.Dapper.StrongIdTypeHandler_Tests;

public class GetValueAsType_Tests
{
	[Fact]
	public void Value_Is_Correct_Type__Returns_Value()
	{
		// Arrange
		object value = Rnd.Guid;
		var handler = new StrongIdTypeHandler<TestGuidId>();
		var parse = Substitute.For<Func<string?, Maybe<Guid>>>();

		// Act
		var result = handler.GetValueAsType(value, parse, Rnd.Guid);

		// Assert
		Assert.Equal(value, result);
		parse.DidNotReceiveWithAnyArgs().Invoke(default);
	}

	[Fact]
	public void Value_Is_Not_Correct_Type__Calls_Parse__Receives_Some__Returns_Result()
	{
		// Arrange
		var value = Rnd.Guid;
		var handler = new StrongIdTypeHandler<TestGuidId>();
		var parse = Substitute.For<Func<string?, Maybe<Guid>>>();
		parse.Invoke(default)
			.ReturnsForAnyArgs(value);

		// Act
		var result = handler.GetValueAsType(Rnd.Str, parse, Rnd.Guid);

		// Assert
		Assert.Equal(value, result);
	}

	[Fact]
	public void Value_Is_Not_Correct_Type__Calls_Parse__Receives_None__Returns_Default_Value()
	{
		// Arrange
		var value = Rnd.Guid;
		var handler = new StrongIdTypeHandler<TestGuidId>();
		var parse = Substitute.For<Func<string?, Maybe<Guid>>>();
		parse.Invoke(default)
			.ReturnsForAnyArgs(Create.None<Guid>());

		// Act
		var result = handler.GetValueAsType(Rnd.Str, parse, value);

		// Assert
		Assert.Equal(value, result);
	}

	[Fact]
	public void Value_Is_Invalid__Calls_Parse__Receives_None__Returns_Default_Value()
	{
		// Arrange
		var value = Rnd.Guid;
		var handler = new StrongIdTypeHandler<TestGuidId>();

		// Act
		var result = handler.GetValueAsType(Rnd.Str, F.ParseGuid, value);

		// Assert
		Assert.Equal(value, result);
	}

	public sealed record class TestGuidId : GuidId;
}
