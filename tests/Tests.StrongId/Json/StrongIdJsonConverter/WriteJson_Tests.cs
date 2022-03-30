// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System.Text.Json;

namespace StrongId.Json.StrongIdJsonConverter_Tests;

public class WriteJson_Tests : Json_Tests
{
	[Fact]
	public void Serialise_Value_Returns_Json_Value()
	{
		// Arrange
		var value = Rnd.Lng;
		var id = new TestId { Value = value };

		// Act
		var result = JsonSerializer.Serialize(id, Options);

		// Assert
		Assert.Equal($"\"{value}\"", result);
	}

	[Theory]
	[InlineData(null)]
	public void Serialise_Null_Returns_Null(TestId? input)
	{
		// Arrange

		// Act
		var result = JsonSerializer.Serialize(input, Options);

		// Assert
		Assert.Equal("null", result);
	}

	public sealed record class TestId : LongId;
}
