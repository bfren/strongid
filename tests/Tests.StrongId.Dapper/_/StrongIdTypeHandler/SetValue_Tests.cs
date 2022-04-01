// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System.Data;
using static StrongId.Testing.Generator;

namespace StrongId.Dapper.StrongIdTypeHandler_Tests;

public class SetValue_Tests
{
	private void Sets_Parameter__With_Correct_Type_And_Value<T>(T id, DbType expectedType)
		where T : class, IStrongId, new()
	{
		// Arrange
		var handler = new StrongIdTypeHandler<T>();
		var parameter = Substitute.For<IDbDataParameter>();

		// Act
		handler.SetValue(parameter, id);

		// Assert
		Assert.Equal(expectedType, parameter.DbType);
		Assert.Equal(id.Value, parameter.Value);
	}

	[Fact]
	public void Sets_Parameter__Guid() =>
		Sets_Parameter__With_Correct_Type_And_Value(GuidId<TestGuidId>(), DbType.Guid);

	[Fact]
	public void Sets_Parameter__Int32() =>
		Sets_Parameter__With_Correct_Type_And_Value(IntId<TestIntId>(), DbType.Int32);

	[Fact]
	public void Sets_Parameter__Int64() =>
		Sets_Parameter__With_Correct_Type_And_Value(LongId<TestLongId>(), DbType.Int64);

	public sealed record class TestGuidId : GuidId;

	public sealed record class TestIntId : IntId;

	public sealed record class TestLongId : LongId;
}
