// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId.Mvc.ModelBinding.LongIdModelBinder_Tests;

public class Default_Tests
{
	[Fact]
	public void Returns_Zero()
	{
		// Arrange
		var binder = new LongIdModelBinder<TestLongId>();

		// Act
		var result = binder.Default;

		// Assert
		Assert.Equal(0L, Assert.IsType<long>(result));
	}

	public sealed record class TestLongId : LongId;
}
