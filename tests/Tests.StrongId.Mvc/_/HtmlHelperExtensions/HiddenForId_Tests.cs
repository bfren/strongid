// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using static StrongId.Testing.Generator;

namespace StrongId.Mvc.HtmlHelperExtensions_Tests;

public class HiddenForId_Tests
{
	private (IHtmlHelper<TestModel>, Vars) Setup()
	{
		var id = GuidId<TestId>();
		var model = new TestModel(id);

		var metadata = Substitute.ForPartsOf<ModelMetadata>(ModelMetadataIdentity.ForType(typeof(TestModel)));
		var provider = Substitute.For<IModelMetadataProvider>();
		provider.GetMetadataForType(typeof(TestModel)).Returns(metadata);

		var dictionary = new ModelStateDictionary();
		var viewData = new ViewDataDictionary<TestModel>(provider, dictionary) { Model = model };

		var htmlHelper = Substitute.For<IHtmlHelper<TestModel>>();
		htmlHelper.ViewData
			.Returns(viewData);

		return (htmlHelper, new(x => x.Id, id, model, viewData));
	}

	private sealed record class Vars(
		Expression<Func<TestModel, TestId?>> Expression,
		TestId Id,
		TestModel Model,
		ViewDataDictionary<TestModel> ViewData
	);

	[Fact]
	public void Calls_NameFor__With_Correct_Values()
	{
		// Arrange
		var (htmlHelper, v) = Setup();

		// Act
		_ = htmlHelper.HiddenForId(v.Expression);

		// Assert
		htmlHelper.Received().NameFor(v.Expression);
	}

	[Fact]
	public void Model_Is_Correct_Type__Calls_Hidden__With_Correct_Values()
	{
		// Arrange
		var (htmlHelper, v) = Setup();
		var name = Rnd.Str;
		htmlHelper.NameFor(v.Expression)
			.Returns(name);

		// Act
		_ = htmlHelper.HiddenForId(v.Expression);

		// Assert
		htmlHelper.Received().Hidden(name, v.Id.Value, null);
	}

	[Fact]
	public void Model_Is_Null__Calls_Hidden__With_Null_Value()
	{
		// Arrange
		var (htmlHelper, v) = Setup();
		var name = Rnd.Str;
		htmlHelper.NameFor(v.Expression)
			.Returns(name);
		v.ViewData.Model = null!;

		// Act
		_ = htmlHelper.HiddenForId(v.Expression);

		// Assert
		htmlHelper.Received().Hidden(name, null, null);
	}

	public sealed record class TestModel(TestId Id);

	public sealed record class TestId : GuidId;
}
