// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId.Testing.Generator_Tests;

public class GuidId_Tests
{
	[Fact]
	public void Returns_Different_Value_Each_Time()
	{
		// Arrange
		var iterations = 10000;
		var values = new List<Guid>();

		// Act
		for (var i = 0; i < iterations; i++)
		{
			values.Add(Generator.GuidId<TestId>().Value);
		}

		var result = values.Distinct().Count();

		// Assert
		Assert.Equal(values.Count, result);
	}

	public sealed record class TestId : GuidId;
}
