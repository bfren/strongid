// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId.Mvc.ModelBinding.ULongIdModelBinder_Tests;

public class Default_Tests
{
	[Fact]
	public void Returns_Zero()
	{
		// Arrange
		var binder = new ULongIdModelBinder<TestULongId>();

		// Act
		var result = binder.Default;

		// Assert
		Assert.Equal(0UL, Assert.IsType<ulong>(result));
	}

	public sealed record class TestULongId : ULongId;
}
