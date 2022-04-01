// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System.Text.Json;
using static StrongId.Testing.Generator;

namespace StrongId.Json.StrongIdJsonConverter_Tests;

public class WriteJson_Tests
{
	[Fact]
	public void Serialise_Value_Returns_Json_Value()
	{
		// Arrange
		var guidId = GuidId<TestGuidId>();
		var intId = IntId<TestIntId>();
		var longId = LongId<TestLongId>();

		// Act
		var r0 = JsonSerializer.Serialize(guidId, Helpers.Options);
		var r1 = JsonSerializer.Serialize(intId, Helpers.Options);
		var r2 = JsonSerializer.Serialize(longId, Helpers.Options);

		// Assert
		Assert.Equal($"\"{guidId.Value}\"", r0);
		Assert.Equal($"\"{intId.Value}\"", r1);
		Assert.Equal($"\"{longId.Value}\"", r2);
	}

	[Theory]
	[InlineData(null, null, null)]
	public void Serialise_Null_Returns_Null(TestGuidId? guidId, TestIntId? intId, TestLongId? longId)
	{
		// Arrange

		// Act
		var r0 = JsonSerializer.Serialize(guidId, Helpers.Options);
		var r1 = JsonSerializer.Serialize(intId, Helpers.Options);
		var r2 = JsonSerializer.Serialize(longId, Helpers.Options);

		// Assert
		Assert.Equal("null", r0);
		Assert.Equal("null", r1);
		Assert.Equal("null", r2);
	}

	public sealed record class TestGuidId : GuidId;

	public sealed record class TestIntId : IntId;

	public sealed record class TestLongId : LongId;
}
