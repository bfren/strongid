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

	public static IEnumerable<object[]> Valid_Numeric_Json_Data()
	{
		yield return new object[] { "{0}" };
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
