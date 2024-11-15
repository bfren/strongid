// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StrongId.Newtonsoft.Json;

public static class Helpers
{
	public static JsonSerializerSettings Settings
	{
		get
		{
			var opt = new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			};
			opt.Converters.Add(new StrongIdJsonConverter());
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
