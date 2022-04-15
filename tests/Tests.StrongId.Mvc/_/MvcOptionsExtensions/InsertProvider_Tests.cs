// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using Microsoft.AspNetCore.Mvc.ModelBinding;
using StrongId.Mvc.ModelBinding;

namespace StrongId.Mvc.MvcOptionsExtensions_Tests;

public class InsertProvider_Tests
{
	[Fact]
	public void Inserts_ModelBinderProvider__As_First_Item()
	{
		// Arrange
		var insert = Substitute.For<Action<int, IModelBinderProvider>>();

		// Act
		MvcOptionsExtensions.InsertProvider(insert);

		// Assert
		insert.Received().Invoke(0, Arg.Any<StrongIdModelBinderProvider>());
	}
}
