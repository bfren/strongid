// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using static StrongId.Mvc.ModelBinding.IntIdModelBinder_Tests.Parse_Tests;

namespace StrongId.Mvc.ModelBinding.LongIdModelBinder_Tests;

public class Parse_Tests : Abstracts.Parse_Tests<LongIdModelBinder<Parse_Tests.TestLongId>, Parse_Tests.TestLongId, long>
{
	public static TheoryData<string?> Extreme_Long_Input =>
		[
			long.MinValue.ToString(),
			long.MaxValue.ToString()
		];

	[Theory]
	[MemberData(nameof(Valid_Integer_Input), MemberType = typeof(IntIdModelBinder_Tests.Parse_Tests))]
	[MemberData(nameof(Extreme_Long_Input))]
	public override void Test00_Valid_Input_Returns_Parsed_Result(string? input)
	{
		Test00(input, long.Parse);
	}

	[Theory]
	[MemberData(nameof(Invalid_Integer_Input), MemberType = typeof(IntIdModelBinder_Tests.Parse_Tests))]
	public override void Test01_Invalid_Input_Returns_None_With_UnableToParseValueAsMsg(string? input)
	{
		Test01(input);
	}

	[Theory]
	[InlineData(null)]
	public override void Test02_Null_Input_Returns_None_With_UnableToParseValueAsMsg(string? input)
	{
		Test02(input);
	}

	public sealed record class TestLongId : LongId;
}
