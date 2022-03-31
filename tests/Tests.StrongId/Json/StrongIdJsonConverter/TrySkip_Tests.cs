// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System.Text.Json;

namespace StrongId.Json.StrongIdJsonConverter_Tests;

public class TrySkip_Tests
{
	[Fact]
	public void Skipped_True__Returns_DefaultValue()
	{
		// Arrange
		var defaultValue = Rnd.Lng;
		var converter = Substitute.ForPartsOf<StrongIdJsonConverter<TestId>>();

		// Act
		var result = converter.HandleSkip(true, defaultValue);

		// Assert
		Assert.Equal(defaultValue, result);
	}

	[Fact]
	public void Skipped_False__Throws_JsonException()
	{
		// Arrange
		var converter = Substitute.ForPartsOf<StrongIdJsonConverter<TestId>>();

		// Act
		var action = void () => converter.HandleSkip(false, Rnd.Lng);

		// Assert
		var ex = Assert.Throws<JsonException>(action);
		Assert.Equal($"Invalid {typeof(long)} and unable to skip reading current token.", ex.Message);
	}

	public sealed record class TestId : LongId;
}
