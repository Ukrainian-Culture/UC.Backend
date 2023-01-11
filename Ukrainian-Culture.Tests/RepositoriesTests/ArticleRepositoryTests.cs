namespace Ukrainian_Culture.Tests.RepositoriesTests;

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
        Guid firstArticleId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid secondArticleId = new("5b32effd-1111-4cab-8ac9-3258c746aa53");
        Guid categoryId = new("5b32effd-2636-4cab-8ac9-3258c746aa53");

        _context.Articles.AddRange(new List<Article>()
            {
                new()
                {
                    Id = firstArticleId,
                    CategoryId = categoryId,
                    Date = new DateTime(1, 1, 1),
                    Region = "1",
                    Type = "1"
                },
                new()
                {
                    Id = secondArticleId,
                    CategoryId = categoryId,
                    Date = new DateTime(2, 2, 2),
                    Region = "2",
                    Type = "2"
                }
            }
        );

        _context.Categories.Add(new Category { Id = categoryId });
        await _context.SaveChangesAsync();

        var articleRepository = new ArticleRepository(_context);

        //Act
        var article
            = (await articleRepository.GetAllByConditionAsync(_ => true, ChangesType.AsNoTracking)).ToList();

        //Assert
        article.Should().HaveCount(2);
        article[0].Id.Should().Be(firstArticleId);
        article[1].Id.Should().Be(secondArticleId);
    }

    public static IEnumerable<object[]> TestData()
    {
        Guid firstArticleId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid secondArticleId = new("5b32effd-1111-4cab-8ac9-3258c746aa53");
        Guid categoryId = new("5b32effd-2636-4cab-8ac9-3258c746aa53");

        yield return new object[]
        {
            new Article
            {
                Id = firstArticleId,
                CategoryId = categoryId,
                Date = new DateTime(1, 1, 1),
                Region = "1",
                Type = "1"
            }, firstArticleId
        };

        yield return new object[]
        {
            new Article
            {
                Id = secondArticleId,
                CategoryId = categoryId,
                Date = new DateTime(2, 2, 2),
                Region = "2",
                Type = "2"
            }, secondArticleId
        };
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public async Task GetAllByConditionAsync_SholdReturnPartOfCollection_WhenHasCorrectExpressionAndDbIsNotEmpty(Article expected, Guid idToCompare)
    {
        //Arrange
        Guid firstArticleId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid secondArticleId = new("5b32effd-1111-4cab-8ac9-3258c746aa53");
        Guid categoryId = new("5b32effd-2636-4cab-8ac9-3258c746aa53");

        _context.Articles.AddRange(new List<Article>()
            {
                new ()
                {
                    Id = firstArticleId,
                    CategoryId = categoryId,
                    Date = new DateTime(1, 1, 1),
                    Region = "1",
                    Type = "1"
                },
                new()
                {
                    Id = secondArticleId,
                    CategoryId = categoryId,
                    Date = new DateTime(2, 2, 2),
                    Region = "2",
                    Type = "2"
                }
            }
        );

        _context.Categories.Add(new Category { Id = categoryId });
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
        Guid firstArticleId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid secondArticleId = new("5b32effd-1111-4cab-8ac9-3258c746aa53");
        Guid categoryId = new("5b32effd-2636-4cab-8ac9-3258c746aa53");

        _context.Articles.AddRange(new List<Article>()
            {
                new()
                {
                    Id = firstArticleId,
                    CategoryId = categoryId,
                    Date = new DateTime(1, 1, 1),
                    Region = "1",
                    Type = "1"
                },
                new()
                {
                    Id = secondArticleId,
                    CategoryId = categoryId,
                    Date = new DateTime(2, 2, 2),
                    Region = "2",
                    Type = "2"
                }
            }
        );

        _context.Categories.Add(new Category { Id = categoryId });
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
        Guid firstArticleId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid categoryId = new("5b32effd-2636-4cab-8ac9-3258c746aa53");

        _context.Articles.AddRange(new List<Article>()
            {
                new()
                {
                    Id = firstArticleId,
                    CategoryId = categoryId,
                    Date = new DateTime(1, 1, 1),
                    Region = "1",
                    Type = "1"
                }
            }
        );

        _context.Categories.Add(new Category { Id = categoryId });
        await _context.SaveChangesAsync();

        var articleRepository = new ArticleRepository(_context);

        //Act
        Guid nonExistedId = new("5b32effd-1111-4cab-8ac9-3258c746aa53");
        var article
            = (await articleRepository.GetAllByConditionAsync(art => art.Id == nonExistedId, ChangesType.AsNoTracking)).ToList();

        //Assert
        article.Should().BeEmpty();

    }
}