// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System.Text.Json;
using System.Text.Json.Serialization;
using StrongId.Json;

namespace StrongId;

public static class Helpers
{
	public static JsonSerializerOptions Options
	{
		get
		{
			var opt = new JsonSerializerOptions
			{
				NumberHandling = JsonNumberHandling.AllowReadingFromString
			};

			opt.Converters.Add(new StrongIdJsonConverterFactory());
			return opt;
		}
	}

	public static IEnumerable<object[]> Valid_Numeric_Json_Data()
	{
		yield return new object[] { "{0}" };
		yield return new object[] { "\"{0}\"" };
	}

	public static IEnumerable<object[]> Valid_String_Json_Data()
	{
		yield return new object[] { "\"{0}\"" };
	}

	public static IEnumerable<object[]> Invalid_Json_Data()
	{
		yield return new object[] { "\"  \"" };
		yield return new object[] { "true" };
		yield return new object[] { "false" };
		yield return new object[] { "[ 0, 1, 2 ]" };
		yield return new object[] {/*lang=json,strict*/ "{ \"foo\": \"bar\" }" };
	}
}
