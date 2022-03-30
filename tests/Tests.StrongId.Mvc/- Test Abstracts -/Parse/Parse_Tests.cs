// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using StrongId;
using StrongId.Mvc;
using static MaybeF.F.R;

namespace Abstracts;

public abstract class Parse_Tests<TBinder, TId, TIdValue>
	where TBinder : StrongIdModelBinder<TId, TIdValue>, new()
	where TId : class, IStrongId<TIdValue>, new()
{
	public abstract void Test00_Valid_Input_Returns_Parsed_Result(string? input);

	protected static void Test00(string? input, Func<string, TIdValue> getExpected)
	{
		// Arrange
		var expected = getExpected(input ?? string.Empty);
		var binder = new TBinder();

		// Act
		var result = binder.Parse(input);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(expected, some);
	}

	public abstract void Test01_Invalid_Input_Returns_None_With_UnableToParseValueAsReason(string? input);

	protected static void Test01(string? input)
	{
		// Arrange
		var binder = new TBinder();

		// Act
		var result = binder.Parse(input);

		// Assert
		var msg = result.AssertNone().AssertType<UnableToParseValueAsReason>();
		Assert.Equal(typeof(TIdValue), msg.Type);
		Assert.Equal(input, msg.Value);
	}

	public abstract void Test02_Null_Input_Returns_None_With_UnableToParseValueAsReason(string? input);

	protected static void Test02(string? input)
	{
		// Arrange
		var binder = new TBinder();

		// Act
		var result = binder.Parse(input);

		// Assert
		var msg = result.AssertNone().AssertType<UnableToParseValueAsReason>();
		Assert.Equal(typeof(TIdValue), msg.Type);
		Assert.Empty(msg.Value);
	}
}
