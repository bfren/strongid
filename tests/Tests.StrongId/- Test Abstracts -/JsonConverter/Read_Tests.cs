// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System.Text.Json;
using StrongId;

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
		var result = JsonSerializer.Deserialize<TId>(json, Helpers.Options);

		// Assert
		Assert.Equal(value, result!.Value);
	}

	public abstract void Test01_Deserialise__Valid_Json__Returns_Object_With_Id_Value(string format);

	protected static void Test01(string format, TIdValue value)
	{
		// Arrange
		var id = Rnd.Int;
		var json = $"{{ \"Id\": {id}, \"WrappedId\": {string.Format(format, value)} }}";

		// Act
		var result = JsonSerializer.Deserialize<TestWrappedId>(json, Helpers.Options);

		// Assert
		Assert.Equal(id, result!.Id);
		Assert.Equal(value, result.WrappedId.Value);
	}

	public abstract void Test02_Deserialise__Null_Or_Invalid_Json__Returns_Default_Id_Value(string input);

	protected static void Test02(string input, TIdValue defaultValue)
	{
		// Arrange

		// Act
		var result = JsonSerializer.Deserialize<TId>(input, Helpers.Options);

		// Assert
		Assert.Equal(defaultValue, result!.Value);
	}

	public abstract void Test03_Deserialise__Null_Or_Invalid_Json__Returns_Object_With_Default_Id_Value(string input);

	protected static void Test03(string input, TIdValue defaultValue)
	{
		// Arrange
		var id = Rnd.Int;
		var json = $"{{ \"Id\": {id}, \"WrappedId\": {input} }}";

		// Act
		var result = JsonSerializer.Deserialize<TestWrappedId>(json, Helpers.Options);

		// Assert
		Assert.Equal(id, result!.Id);
		Assert.Equal(defaultValue, result.WrappedId.Value);
	}

	public sealed record class TestWrappedId(int Id, TId WrappedId);
}
