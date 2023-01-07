
namespace Ukrainian_Culture.Tests.RepostoryTests;

public class ArticleRepositoryTests
{
    private readonly RepositoryContext _context;
    public ArticleRepositoryTests()
    {
        using var factory = new ConnectionFactory();
        _context = factory.CreateContextForInMemory(new ArticleModel());
    }

    public static IEnumerable<object[]> TestData()
    {
        yield return new object[]
        {
            new Article
            {
                Id = 0,
                Type = "file",
                Region = "Kyiv",
                Date = new DateTime(2003, 01, 01),
                CategoryId = 1,
            }, 0
        };

        yield return new object[]
        {
            new Article
            {
                Id = 1,
                Type = "text",
                Region = "Kyiv",
                Date = new DateTime(2002, 01, 01),
                CategoryId = 1,
            }, 1
        };
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public async Task GetArticlesByConditionAsync_SholdReturnCollectionOfAllElements_WhenExpressionIsEqualToTrueAndDbIsNotEmpty(Article expected, int IdExpected)
    {
        //Arrange
        _context.Articles.AddRange(new List<Article>()
            {
                new ()
                {
                    Id = 0,
                    Type = "file",
                    Region = "Kyiv",
                    Date = new DateTime(2003, 01, 01),
                    CategoryId = 1,
                },
                new()
                {
                    Id = 1,
                    Type = "text",
                    Region = "Kyiv",
                    Date = new DateTime(2002, 01, 01),
                    CategoryId = 1,
                }
            }
        );

        _context.Categories.Add(new Category { Id = 1 });
        await _context.SaveChangesAsync();

        var articleRepository = new ArticleRepository(_context);

        //Act
        var article = (await articleRepository.GetArticlesByConditionAsync(_ => true, ChangesType.AsNoTracking))
            .ToList();

        //Assert
        article.Should().HaveCount(2);
        article.Should().ContainEquivalentOf(expected);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public async Task GetArticlesByConditionAsync_SholdReturnCollectionOfNotAllElements_WhenHasCorrectExpressionAndDbIsNotEmpty(Article expected, int IdExpected)
    {
        //Arrange
        _context.Articles.AddRange(new List<Article>()
            {
                new ()
                {
                    Id = 0,
                    Type = "file",
                    Region = "Kyiv",
                    Date = new DateTime(2003, 01, 01),
                    CategoryId = 1,
                },
                new()
                {
                    Id = 1,
                    Type = "text",
                    Region = "Kyiv",
                    Date = new DateTime(2002, 01, 01),
                    CategoryId = 1,
                }
            }
        );

        _context.Categories.Add(new Category { Id = 1 });
        await _context.SaveChangesAsync();

        var articleRepository = new ArticleRepository(_context);

        //Act
        var article = (await articleRepository.GetArticlesByConditionAsync(art => art.Id == IdExpected, ChangesType.AsNoTracking))
            .ToList();

        //Assert
        article.Should().HaveCount(1);

        article[0].Should()
            .Match<Article>(art => art.Id == expected.Id &&
                                   art.CategoryId == expected.CategoryId &&
                                   art.Region == expected.Region &&
                                   art.Type == expected.Type);
    }

    [Fact]
    public async Task GetArticlesByConditionAsync_SholdReturnEmptyCollection_WhenHasIncorrectExpressionAndDbIsNotEmpty()
    {
        //Arrange
        _context.Articles.AddRange(new List<Article>()
            {
                new ()
                {
                    Id = 0,
                    Type = "file",
                    Region = "Kyiv",
                    Date = new DateTime(2003, 01, 01),
                    CategoryId = 1,
                },
                new()
                {
                    Id = 1,
                    Type = "text",
                    Region = "Kyiv",
                    Date = new DateTime(2002, 01, 01),
                    CategoryId = 1,
                }
            }
       );

        _context.Categories.Add(new Category { Id = 1 });
        await _context.SaveChangesAsync();

        var articleRepository = new ArticleRepository(_context);

        var IdUnexpected = 2;

        //Act
        var article = (await articleRepository.GetArticlesByConditionAsync( art => art.Id == IdUnexpected, ChangesType.AsNoTracking))
            .ToList();

        //Assert
        article.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public async Task GetArticlesByConditionAsync_SholdReturnEmptyCollection_WhenDbIsEmpty(Article expected, int IdExpected)
    {
        //Arrange
        _context.Articles.AddRange(new List<Article>() {});

        var articleRepository = new ArticleRepository(_context);

        //Act
        var article = (await articleRepository.GetArticlesByConditionAsync(art => art.Id == IdExpected, ChangesType.AsNoTracking))
            .ToList();

        //Assert
        article.Should().BeEmpty();
    }
}