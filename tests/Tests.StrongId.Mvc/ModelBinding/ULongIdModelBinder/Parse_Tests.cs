// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using static StrongId.Mvc.ModelBinding.UIntIdModelBinder_Tests.Parse_Tests;

namespace StrongId.Mvc.ModelBinding.ULongIdModelBinder_Tests;

public class Parse_Tests : Abstracts.Parse_Tests<ULongIdModelBinder<Parse_Tests.TestULongId>, Parse_Tests.TestULongId, ulong>
{
	public static TheoryData<string> Extreme_Unsigned_Long_Input =>
		[
			ulong.MinValue.ToString(),
			ulong.MaxValue.ToString()
		];

	[Theory]
	[MemberData(nameof(Valid_Unsigned_Integer_Input), MemberType = typeof(UIntIdModelBinder_Tests.Parse_Tests))]
	[MemberData(nameof(Extreme_Unsigned_Long_Input))]
	public override void Test00_Valid_Input_Returns_Parsed_Result(string? input)
	{
		Test00(input, ulong.Parse);
	}

	[Theory]
	[MemberData(nameof(Invalid_Unsigned_Integer_Input), MemberType = typeof(UIntIdModelBinder_Tests.Parse_Tests))]
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

	public sealed record class TestULongId : ULongId;
}
