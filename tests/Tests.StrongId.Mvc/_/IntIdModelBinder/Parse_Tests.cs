
// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using StrongId.Mvc.ModelBinding;

namespace StrongId.Mvc.IntIdModelBinder_Tests;

public class Parse_Tests : Abstracts.Parse_Tests<IntIdModelBinder<Parse_Tests.TestIntId>, Parse_Tests.TestIntId, int>
{
	public static IEnumerable<object[]> Valid_Integer_Input()
	{
		yield return new object[] { "1" };
		yield return new object[] { "-1" };
		yield return new object[] { "  1  " };
		yield return new object[] { "1000" };
		yield return new object[] { "-1000" };
	}

	public static IEnumerable<object[]> Invalid_Integer_Input()
	{
		yield return new object[] { "" };
		yield return new object[] { "Invalid" };
		yield return new object[] { "1-" };
		yield return new object[] { "(1)" };
		yield return new object[] { "1.01" };
		yield return new object[] { "£1" };
		yield return new object[] { "£1.10" };
		yield return new object[] { "1e4" };
		yield return new object[] { "-1e4" };
		yield return new object[] { "1e-4" };
		yield return new object[] { "-1e-4" };
		yield return new object[] { "1,000" };
		yield return new object[] { "-1,000" };
	}

	public static IEnumerable<object[]> Extreme_Int_Input()
	{
		yield return new object[] { int.MinValue.ToString() };
		yield return new object[] { int.MaxValue.ToString() };
	}

	[Theory]
	[MemberData(nameof(Valid_Integer_Input))]
	[MemberData(nameof(Extreme_Int_Input))]
	public override void Test00_Valid_Input_Returns_Parsed_Result(string? input)
	{
		Test00(input, int.Parse);
	}

	[Theory]
	[MemberData(nameof(Invalid_Integer_Input))]
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

	public sealed record class TestIntId : IntId;
}
