// StrongId: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using Microsoft.Extensions.Caching.Memory;

namespace StrongId.Functions.TypeF_Tests;

public class GetOrCreateCacheEntry_Tests
{
	[Fact]
	public void Cache_Entry_Exists__Returns_Entry()
	{
		// Arrange
		var type = typeof(Guid);
		var cache = Substitute.For<IMemoryCache>();
		cache.TryGetValue(type.GetHashCode(), out Arg.Any<object>())
			.Returns(x =>
			{
				x[1] = type;
				return true;
			});

		// Act
		var result = TypeF.GetOrCreateCacheEntry(cache, type);

		// Assert
		Assert.Same(type, result);
	}

	[Fact]
	public void Cache_Entry_Does_Not_Exist__Creates_And_Returns_Entry()
	{
		// Arrange
		var type = typeof(TestGuidId);
		var cache = Substitute.For<IMemoryCache>();

		// Act
		var result = TypeF.GetOrCreateCacheEntry(cache, type);

		// Assert
		cache.Received().CreateEntry(type.GetHashCode());
		Assert.Equal(typeof(Guid), result);
	}

	[Fact]
	public void Locks_Thread()
	{
		// Arrange
		var type = typeof(TestGuidId);
		var created = false;
		var cache = Substitute.For<IMemoryCache>();
		cache.TryGetValue(type.GetHashCode(), out Arg.Any<object>())
			.Returns(x =>
			{
				if (!created)
				{
					x[1] = null;
					return false;
				}

				x[1] = type;
				return true;
			});
		cache.When(x => x.CreateEntry(type.GetHashCode()))
			.Do(Callback.First(_ => created = true));

		// Act
		Parallel.ForEach(Enumerable.Range(0, 10), _ => TypeF.GetOrCreateCacheEntry(cache, type));

		// Assert
		cache.Received(1).CreateEntry(type.GetHashCode());
	}

	public sealed record class TestGuidId : GuidId;
}
