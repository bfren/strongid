// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System.Data;

namespace StrongId.Dapper.StrongIdTypeHandler_Tests;

public class SetValue_Tests
{
	[Fact]
	public void Sets_Parameter_Value_As_UInt64()
	{
		// Arrange
		var handler = new StrongIdTypeHandler<TestId>();
		var value = Rnd.Lng;
		var id = new TestId { Value = value };
		var parameter = Substitute.For<IDbDataParameter>();

		// Act
		handler.SetValue(parameter, id);

		// Assert
		Assert.Equal(parameter.Value, value);
	}

	public sealed record class TestId : LongId;
}