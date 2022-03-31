// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System.Text.Json;

namespace StrongId.Json.IntIdJsonConverter_Tests;

public class Read_Tests : Json_Tests
{
	[Theory]
	[InlineData("{0}")]
	[InlineData("\"{0}\"")]
	public void Deserialise__Valid_Json__Returns_IntId_With_Value(string format)
	{
		// Arrange
		var value = Rnd.Lng;
		var json = string.Format(format, value);

		// Act
		var result = JsonSerializer.Deserialize<TestIntId>(json, Options);

		// Assert
		Assert.Equal(value, result!.Value);
	}

	[Theory]
	[InlineData("{0}")]
	[InlineData("\"{0}\"")]
	public void Deserialise__Valid_Json__Returns_Object_With_IntId_Value(string format)
	{
		// Arrange
		var v0 = Rnd.Int;
		var v1 = Rnd.Lng;
		var json = $"{{ \"Id\": {v0}, \"IntId\": {string.Format(format, v1)} }}";

		// Act
		var result = JsonSerializer.Deserialize<IntIdWrapperTest>(json, Options);

		// Assert
		Assert.Equal(v0, result!.Id);
		Assert.Equal(v1, result.IntId.Value);
	}

	[Theory]
	[InlineData("\"  \"")]
	[InlineData("true")]
	[InlineData("false")]
	public void Deserialise__Null_Or_Invalid_Json__Returns_Default_IntId_Value(string input)
	{
		// Arrange

		// Act
		var result = JsonSerializer.Deserialize<TestIntId>(input, Options);

		// Assert
		Assert.Equal(0, result!.Value);
	}

	[Theory]
	[InlineData("\"  \"")]
	[InlineData("true")]
	[InlineData("false")]
	[InlineData("[ 0, 1, 2 ]")]
	[InlineData(/*lang=json,strict*/ "{ \"foo\": \"bar\" }")]
	public void Deserialise__Null_Or_Invalid_Json__Returns_Object_With_Default_IntId_Value(string input)
	{
		// Arrange
		var v0 = Rnd.Int;
		var json = $"{{ \"Id\": {v0}, \"IntId\": {input} }}";

		// Act
		var result = JsonSerializer.Deserialize<IntIdWrapperTest>(json, Options);

		// Assert
		Assert.Equal(v0, result!.Id);
		Assert.Equal(0, result.IntId.Value);
	}

	public sealed record class TestIntId : IntId;

	public class IntIdWrapperTest
	{
		public int Id { get; set; }

		public TestIntId IntId { get; set; } = new();
	}
}
