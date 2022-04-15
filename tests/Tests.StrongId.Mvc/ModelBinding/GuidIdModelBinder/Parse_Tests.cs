// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId.Mvc.ModelBinding.GuidIdModelBinder_Tests;

public class Parse_Tests : Abstracts.Parse_Tests<GuidIdModelBinder<Parse_Tests.TestGuidId>, Parse_Tests.TestGuidId, Guid>
{
	[Theory]
	[InlineData("00000000-0000-0000-0000-000000000000")]
	[InlineData("00000000000000000000000000000000")]
	[InlineData("e402617b-d4fa-4abe-81d7-952695860b51")]
	[InlineData("e402617bd4fa4abe81d7952695860b51")]
	public override void Test00_Valid_Input_Returns_Parsed_Result(string? input)
	{
		Test00(input, Guid.Parse);
	}

	[Theory]
	[InlineData("")]
	[InlineData("Invalid")]
	[InlineData("0")]
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

	public sealed record class TestGuidId : GuidId;
}
