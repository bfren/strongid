// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId.Mvc.ModelBinding.UIntIdModelBinder_Tests;

public class Parse_Tests : Abstracts.Parse_Tests<UIntIdModelBinder<Parse_Tests.TestUIntId>, Parse_Tests.TestUIntId, uint>
{
	public static TheoryData<string?> Valid_Unsigned_Integer_Input =>
		[
			"1",
			"  1  ",
			"1000",
		];

	public static TheoryData<string?> Invalid_Unsigned_Integer_Input =>
		[
			"",
			"Invalid",
			"-1",
			"1-",
			"(1)",
			"1.01",
			"£1",
			"£1.10",
			"1e4",
			"-1e4",
			"1e-4",
			"-1e-4",
			"-1000",
			"1,000",
			"-1,000"
		];

	public static TheoryData<string> Extreme_Unsigned_Integer_Input =>
		[
			uint.MinValue.ToString(),
			uint.MaxValue.ToString()
		];

	[Theory]
	[MemberData(nameof(Valid_Unsigned_Integer_Input))]
	[MemberData(nameof(Extreme_Unsigned_Integer_Input))]
	public override void Test00_Valid_Input_Returns_Parsed_Result(string? input)
	{
		Test00(input, uint.Parse);
	}

	[Theory]
	[MemberData(nameof(Invalid_Unsigned_Integer_Input))]
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

	public sealed record class TestUIntId : UIntId;
}
