// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId.Testing.Generator_Tests;

public class ULongId_Tests
{
	[Fact]
	public void Returns_Different_Value_Each_Time()
	{
		// Arrange
		var iterations = 1000;
		var values = new List<ulong>();

		// Act
		for (var i = 0; i < iterations; i++)
		{
			values.Add(Generator.ULongId<TestId>(false).Value);
		}

		var result = values.Distinct().Count();

		// Assert
		Assert.Equal(values.Count, result);
	}

	public sealed record class TestId : ULongId;
}
