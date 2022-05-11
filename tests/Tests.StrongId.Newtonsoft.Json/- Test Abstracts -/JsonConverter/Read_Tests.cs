// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using Newtonsoft.Json;
using StrongId;
using StrongId.Newtonsoft.Json;

namespace Abstracts;

public abstract class Read_Tests<TId, TIdValue>
	where TId : StrongId<TIdValue>, new()
{
	public abstract void Test00_Deserialise__Valid_Json__Returns_Id_With_Value(string format);

	protected static void Test00(string format, TIdValue value)
	{
		// Arrange
		var json = string.Format(format, value);

		// Act
		var result = JsonConvert.DeserializeObject<TId>(json, Helpers.Settings);

		// Assert
		Assert.Equal(value, result!.Value);
	}

	public abstract void Test01_Deserialise__Valid_Json__Returns_Object_With_Id_Value(string format);

	protected static void Test01(string format, TIdValue value)
	{
		// Arrange
		var id = Rnd.Int;
		var json = $"{{ \"id\": {id}, \"wrappedId\": {string.Format(format, value)} }}";

		// Act
		var result = JsonConvert.DeserializeObject<TestWrappedId>(json, Helpers.Settings);

		// Assert
		Assert.Equal(id, result!.Id);
		Assert.Equal(value, result.WrappedId.Value);
	}

	public abstract void Test02_Deserialise__Null_Or_Invalid_Json__Returns_Default_Id_Value(string input);

	protected static void Test02(string input, TIdValue defaultValue)
	{
		// Arrange

		// Act
		var result = JsonConvert.DeserializeObject<TId>(input, Helpers.Settings);

		// Assert
		Assert.Equal(defaultValue, result!.Value);
	}

	public abstract void Test03_Deserialise__Null_Or_Invalid_Json__Returns_Object_With_Default_Id_Value(string input);

	protected static void Test03(string input, TIdValue defaultValue)
	{
		// Arrange
		var id = Rnd.Int;
		var json = $"{{ \"id\": {id}, \"wrappedId\": {input} }}";

		// Act
		var result = JsonConvert.DeserializeObject<TestWrappedId>(json, Helpers.Settings);

		// Assert
		Assert.Equal(id, result!.Id);
		Assert.Equal(defaultValue, result.WrappedId.Value);
	}

	public sealed record class TestWrappedId(int Id, TId WrappedId);
}
