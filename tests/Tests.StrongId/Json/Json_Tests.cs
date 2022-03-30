// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System.Text.Json;
using System.Text.Json.Serialization;

namespace StrongId.Json;

public abstract class Json_Tests
{
	protected static JsonSerializerOptions Options
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
}
