using Contracts;
using Entities;
using Entities.Models;
using FluentAssertions;
using Repositories;
using Ukrainian_Culture.Tests.DbModels;

namespace Ukrainian_Culture.Tests;

public class ArticleRepositoryTests
{
    private readonly RepositoryContext _context;
    public ArticleRepositoryTests()
    {
        using var factory = new ConnectionFactory();
        _context = factory.CreateContextForInMemory(new ArticleModel());
    }

    [Fact]
    public async Task GetAllByConditionAsync_SholdReturnAllCollection_WhenExpressionIsEqualToTrueAndDbIsNotEmpty()
    {
        //Arrange
        _context.Articles.AddRange(new List<Article>()
            {
                new()
                {
                    Id = 1,
                    CategoryId = 1,
                    Date = new DateTime(1, 1, 1),
                    Region = "1",
                    Type = "1"
                },
                new()
                {
                    Id = 2,
                    CategoryId = 1,
                    Date = new DateTime(2, 2, 2),
                    Region = "2",
                    Type = "2"
                }
            }
        );

        _context.Categories.Add(new Category { Id = 1 });
        await _context.SaveChangesAsync();

        var articleRepository = new ArticleRepository(_context);

        //Act
        var article
            = (await articleRepository.GetAllByConditionAsync(_ => true, ChangesType.AsNoTracking)).ToList();

        //Assert
        article.Should().HaveCount(2);
        article[0].Id.Should().Be(1);
        article[1].Id.Should().Be(2);
    }


    [Theory]
    [MemberData(nameof(TestData))]
    public async Task GetAllByConditionAsync_SholdReturnPartOfCollection_WhenHasCorrectExpressionAndDbIsNotEmpty(Article expected, int idToCompare)
    {
        //Arrange
        _context.Articles.AddRange(new List<Article>()
            {
                new ()
                {
                    Id = 1,
                    CategoryId = 1,
                    Date = new DateTime(1, 1, 1),
                    Region = "1",
                    Type = "1"
                },
                new()
                {
                    Id = 2,
                    CategoryId = 1,
                    Date = new DateTime(2, 2, 2),
                    Region = "2",
                    Type = "2"
                }
            }
        );

        _context.Categories.Add(new Category { Id = 1 });
        await _context.SaveChangesAsync();

        var articleRepository = new ArticleRepository(_context);

        //Act
        var article
            = (await articleRepository.GetAllByConditionAsync(art => art.Id == idToCompare, ChangesType.AsNoTracking)).ToList();

        //Assert
        article.Should().HaveCount(1);

        article[0].Should()
            .Match<Article>(art => art.Id == expected.Id &&
                                   art.CategoryId == expected.CategoryId &&
                                   art.Region == expected.Region &&
                                   art.Type == expected.Type);
    }
    public static IEnumerable<object[]> TestData()
    {
        yield return new object[]
        {
            new Article
            {
                Id = 1,
                CategoryId = 1,
                Date = new DateTime(1, 1, 1),
                Region = "1",
                Type = "1"
            }, 1
        };

        yield return new object[]
        {
            new Article
            {
                Id = 2,
                CategoryId = 1,
                Date = new DateTime(2, 2, 2),
                Region = "2",
                Type = "2"
            }, 2
        };
    }

    [Fact]
    public async Task GetAllByConditionAsync_SholdReturnEmptyCollection_WhenDbIsEmpty()
    {
        //Arrange
        var articleRepository = new ArticleRepository(_context);

        //Act
        var article
            = (await articleRepository.GetAllByConditionAsync(_ => true, ChangesType.AsNoTracking)).ToList();

        //Assert
        article.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllByConditionAsync_SholdReturnEmptyCollection_WhenExpressionIsEqualToFalseAndDbIsNotEmpty()
    {
        //Arrange
        _context.Articles.AddRange(new List<Article>()
            {
                new()
                {
                    Id = 1,
                    CategoryId = 1,
                    Date = new DateTime(1, 1, 1),
                    Region = "1",
                    Type = "1"
                },
                new()
                {
                    Id = 2,
                    CategoryId = 1,
                    Date = new DateTime(2, 2, 2),
                    Region = "2",
                    Type = "2"
                }
            }
        );

        _context.Categories.Add(new Category { Id = 1 });
        await _context.SaveChangesAsync();

        var articleRepository = new ArticleRepository(_context);

        //Act
        var article
            = (await articleRepository.GetAllByConditionAsync(_ => false, ChangesType.AsNoTracking)).ToList();

        //Assert
        article.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllByConditionAsync_SholdReturnEmptyCollection_WhenExpressionIsUncorrectAndDbIsNotEmpty()
    {
        //Arrange
        _context.Articles.AddRange(new List<Article>()
            {
                new()
                {
                    Id = 1,
                    CategoryId = 1,
                    Date = new DateTime(1, 1, 1),
                    Region = "1",
                    Type = "1"
                }
            }
        );

        _context.Categories.Add(new Category { Id = 1 });
        await _context.SaveChangesAsync();

        var articleRepository = new ArticleRepository(_context);

        //Act
        int nonExistedId = 2;
        var article
            = (await articleRepository.GetAllByConditionAsync(art => art.Id == nonExistedId, ChangesType.AsNoTracking)).ToList();

        //Assert
        article.Should().BeEmpty();

    }
}