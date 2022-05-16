// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId.Testing.Generator_Tests;

public class UIntId_Tests
{
	[Fact]
	public void Returns_Different_Value_Each_Time()
	{
		// Arrange
		var iterations = 1000;
		var values = new List<uint>();

		// Act
		for (var i = 0; i < iterations; i++)
		{
			values.Add(Generator.UIntId<TestId>(false).Value);
		}

		var result = values.Distinct().Count();

		// Assert
		Assert.Equal(values.Count, result);
	}

	public sealed record class TestId : UIntId;
}
