// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

namespace StrongId.Functions.TypeF_Tests;

public class GetValueTypeOrNull_Tests
{
	[Fact]
	public void Type_Is_Not_Assignable_From_IStrongId__Returns_Null()
	{
		// Arrange
		var type = typeof(GetValueTypeOrNull_Tests);

		// Act
		var result = TypeF.GetValueTypeOrNull(type);

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public void Type_Does_Not_Implement_IStrongId_With_Value_Type__Returns_Null()
	{
		// Arrange
		var type = typeof(TestIdWithoutValueType);

		// Act
		var result = TypeF.GetValueTypeOrNull(type);

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public void Type_Implements_Multiple_IStrongId_Value_Types__Returns_Null()
	{
		// Arrange
		var type = typeof(TestIdWithMultipleValueTypes);

		// Act
		var result = TypeF.GetValueTypeOrNull(type);

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public void Type_Implements_IStrongId_With_Value_Type_Once__Returns_Value_Type()
	{
		// Arrange
		var type = typeof(TestId);

		// Act
		var result = TypeF.GetValueTypeOrNull(type);

		// Assert
		Assert.Equal(typeof(DateTime), result);
	}

	public sealed record class TestIdWithoutValueType(object Value) : IStrongId;

	public sealed record class TestIdWithMultipleValueTypes(object Value) : IStrongId<int>, IStrongId<long>
	{
		int IStrongId<int>.Value
		{
			get => throw new NotImplementedException();
			init => throw new NotImplementedException();
		}

		long IStrongId<long>.Value
		{
			get => throw new NotImplementedException();
			init => throw new NotImplementedException();
		}
	}

	public sealed record class TestId(DateTime Value) : IStrongId<DateTime>;
}
