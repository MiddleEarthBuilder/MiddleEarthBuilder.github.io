using FluentAssertions;
using MiddleEarth.Builder.Application.Domain;

namespace MiddleEarth.Builder.UnitTests;

public class RepositoryTests
{
    [Theory]
    [InlineData("amet")]
    [InlineData("AmEt")]
    [InlineData("    amet")]
    [InlineData("amet   ")]
    [InlineData(" AmeT ")]
    public async Task GetOrCreate_Should_ReturnExistingItem(string key)
    {
        // arrange
        var repository = CreateRepository();

        // act
        var item = repository.GetOrCreate(key);

        // assert
        item.Should().NotBeNull();
        repository.Count.Should().Be(RepositoryCount);
    }

    [Fact]
    public async Task GetOrCreate_Should_CreateNewItem()
    {
        // arrange
        var repository = CreateRepository();

        // act
        var item = repository.GetOrCreate("consetetur");
        var item2 = repository.GetOrCreate("   consETetur   ");

        // assert
        item.Should().NotBeNull();
        item.Should().Be(item2);
        repository.Count.Should().Be(RepositoryCount + 1);
    }

    [Fact]
    public async Task GetAll_Should_FilterResults()
    {
        // arrange
        var repository = CreateRepository();

        // act
        var items = repository.GetAll("lo");

        // assert
        items.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetAll_Should_FilterResults_When_Except()
    {
        // arrange
        var repository = CreateRepository();

        // act
        var items = repository.GetAll("lo", new[] { "lorem" }).ToArray();

        // assert
        items.Should().HaveCount(1);
        items[0].Id.Should().Be("dolor");
    }

    [Fact]
    public async Task GetAll_Should_FilterResults_When_ExceptWithSpace()
    {
        // arrange
        var repository = CreateRepository();

        // act
        var items = repository.GetAll("  loRem ", new[] { "lorem" }, true).ToArray();

        // assert
        items.Should().HaveCount(0);
    }

    [Fact]
    public async Task GetAll_Should_AddSearchTextItem()
    {
        // arrange
        var repository = CreateRepository();

        // act
        var items = repository.GetAll("consetetur", new[] { "lorem" }, true).ToArray();

        // assert
        items.Should().HaveCount(1);
        items[0].Id.Should().Be("consetetur");
        repository.Count.Should().Be(RepositoryCount);
    }

    private const int RepositoryCount = 5;
    private static Repository<TestModel> CreateRepository() =>
        new Repository<TestModel>(s => new TestModel(s), model => model.Id)
            .Load(new[]
            {
                new TestModel("Lorem", 14),
                new TestModel("Ipsum", 21),
                new TestModel("dolor", 33),
                new TestModel(" sit ", 47),
                new TestModel("amet ", 52)
            });

    private record TestModel
    {
        public string Id { get; set; }
        public int Age { get; set; }

        public TestModel(string id)
        {
            Id = id;
        }

        public TestModel(string id, int age)
        {
            Id = id;
            Age = age;
        }
    }
}
