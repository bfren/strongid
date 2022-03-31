// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId.Json.LongIdJsonConverter_Tests;

public class Read_Tests : Abstracts.Read_Tests<Read_Tests.TestLongId, long>
{
	[Theory]
	[MemberData(nameof(Helpers.Valid_Numeric_Json_Data), MemberType = typeof(Helpers))]
	public override void Test00_Deserialise__Valid_Json__Returns_Id_With_Value(string format)
	{
		Test00(format, Rnd.Lng);
	}

	[Theory]
	[MemberData(nameof(Helpers.Valid_Numeric_Json_Data), MemberType = typeof(Helpers))]
	public override void Test01_Deserialise__Valid_Json__Returns_Object_With_Id_Value(string format)
	{
		Test01(format, Rnd.Lng);
	}

	[Theory]
	[MemberData(nameof(Helpers.Invalid_Json_Data), MemberType = typeof(Helpers))]
	public override void Test02_Deserialise__Null_Or_Invalid_Json__Returns_Default_Id_Value(string input)
	{
		Test02(input, 0L);
	}

	[Theory]
	[MemberData(nameof(Helpers.Invalid_Json_Data), MemberType = typeof(Helpers))]
	public override void Test03_Deserialise__Null_Or_Invalid_Json__Returns_Object_With_Default_Id_Value(string input)
	{
		Test03(input, 0L);
	}

	public sealed record class TestLongId : LongId;
}
