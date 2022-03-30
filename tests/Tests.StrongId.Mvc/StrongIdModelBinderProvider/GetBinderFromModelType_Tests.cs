// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StrongId.Mvc.StrongIdModelBinderProvider_Tests;

public class GetBinderFromModelType_Tests
{
	[Fact]
	public void ModelType_Does_Not_Implement_StrongId__Throws_ModelBinderException()
	{
		// Arrange
		var type = typeof(RandomType);

		// Act
		var action = () => StrongIdModelBinderProvider.GetBinderFromModelType(type);

		// Assert
		Assert.Throws<ModelBinderException>(action);
	}

	[Fact]
	public void Unsupported_StrongId_Value_Type__Throws_ModelBinderException()
	{
		// Arrange
		var type = typeof(TestDateTimeId);

		// Act
		var action = () => StrongIdModelBinderProvider.GetBinderFromModelType(type);

		// Assert
		Assert.Throws<ModelBinderException>(action);
	}

	[Fact]
	public void ModelType_Implements_GuidId__Returns_GuidIdModelBinder()
	{
		// Arrange
		var type = typeof(TestGuidId);

		// Act
		var result = StrongIdModelBinderProvider.GetBinderFromModelType(type);

		// Assert
		Assert.IsAssignableFrom<IModelBinder>(result);
		Assert.IsAssignableFrom<StrongIdModelBinder<TestGuidId, Guid>>(result);
	}

	[Fact]
	public void ModelType_Implements_IntId__Returns_IntIdModelBinder()
	{
		// Arrange
		var type = typeof(TestIntId);

		// Act
		var result = StrongIdModelBinderProvider.GetBinderFromModelType(type);

		// Assert
		Assert.IsAssignableFrom<IModelBinder>(result);
		Assert.IsAssignableFrom<StrongIdModelBinder<TestIntId, int>>(result);
	}

	[Fact]
	public void ModelType_Implements_LongId__Returns_LongIdModelBinder()
	{
		// Arrange
		var type = typeof(TestLongId);

		// Act
		var result = StrongIdModelBinderProvider.GetBinderFromModelType(type);

		// Assert
		Assert.IsAssignableFrom<IModelBinder>(result);
		Assert.IsAssignableFrom<StrongIdModelBinder<TestLongId, long>>(result);
	}

	public sealed record class RandomType;

	public sealed record class TestDateTimeId : StrongId<DateTime>;

	public sealed record class TestGuidId : GuidId;

	public sealed record class TestIntId : IntId;

	public sealed record class TestLongId : LongId;
}
