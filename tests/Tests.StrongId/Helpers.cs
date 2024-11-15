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

	public static TheoryData<string> Valid_Numeric_Json_Data =>
		[
			"{0}",
			"\"{0}\""
		];

	public static TheoryData<string> Valid_String_Json_Data =>
		[
			"\"{0}\""
		];

	public static TheoryData<string> Invalid_Json_Data =>
		[
			"\"  \"",
			"true",
			"false",
			"[ 0, 1, 2 ]",
			/*lang=json,strict*/ "{ \"foo\": \"bar\" }"
		];
}
