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
    public async Task GetAllByConditionAsync_ShouldReturnAllCollection_WhenExpressionIsEqualToTrueAndDbIsNotEmpty()
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
                    Region = "1"
                },
                new()
                {
                    Id = secondArticleId,
                    CategoryId = categoryId,
                    Date = new DateTime(2, 2, 2),
                    Region = "2"
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
                Region = "1"
            },
            firstArticleId
        };

        yield return new object[]
        {
            new Article
            {
                Id = secondArticleId,
                CategoryId = categoryId,
                Date = new DateTime(2, 2, 2),
                Region = "2"
            },
            secondArticleId
        };
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public async Task GetAllByConditionAsync_ShouldReturnPartOfCollection_WhenHasCorrectExpressionAndDbIsNotEmpty(
        Article expected, Guid idToCompare)
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
                    Region = "1"
                },
                new()
                {
                    Id = secondArticleId,
                    CategoryId = categoryId,
                    Date = new DateTime(2, 2, 2),
                    Region = "2"
                }
            }
        );

        _context.Categories.Add(new Category { Id = categoryId });
        await _context.SaveChangesAsync();

        var articleRepository = new ArticleRepository(_context);

        //Act
        var article
            = (await articleRepository.GetAllByConditionAsync(art => art.Id == idToCompare, ChangesType.AsNoTracking))
            .ToList();

        //Assert
        article.Should().HaveCount(1);

        article[0].Should()
            .Match<Article>(art => art.Id == expected.Id &&
                                   art.CategoryId == expected.CategoryId &&
                                   art.Region == expected.Region);
    }

    [Fact]
    public async Task GetAllByConditionAsync_ShouldReturnEmptyCollection_WhenDbIsEmpty()
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
    public async Task GetAllByConditionAsync_ShouldReturnEmptyCollection_WhenExpressionIsEqualToFalseAndDbIsNotEmpty()
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
                    Region = "1"
                },
                new()
                {
                    Id = secondArticleId,
                    CategoryId = categoryId,
                    Date = new DateTime(2, 2, 2),
                    Region = "2"
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
    public async Task GetAllByConditionAsync_ShouldReturnEmptyCollection_WhenExpressionIsUncorrectAndDbIsNotEmpty()
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
                    Region = "1"
                }
            }
        );

        _context.Categories.Add(new Category { Id = categoryId });
        await _context.SaveChangesAsync();

        var articleRepository = new ArticleRepository(_context);

        //Act
        Guid nonExistedId = new("5b32effd-1111-4cab-8ac9-3258c746aa53");
        var article
            = (await articleRepository.GetAllByConditionAsync(art => art.Id == nonExistedId, ChangesType.AsNoTracking))
            .ToList();

        //Assert
        article.Should().BeEmpty();
    }

    [Fact]
    public async Task CreateArticle_ShouldCreateNewArticleInDb_WhenCorrectData()
    {
        //Arrange
        ArticleRepository repository = new(_context);

        //Act
        repository.CreateArticle(new Article()
        {
            Id = new Guid("52afd786-d121-4f3c-a033-c77650d48d41")
        });
        await _context.SaveChangesAsync();

        //Assert
        _context.Articles.Should().HaveCount(1);
    }

    [Fact]
    public async Task CreateArticle_ShouldThrowException_WhenIdAlreadyExists()
    {
        //Arrange
        Guid id = new("83bfe807-a48a-42a6-b874-89cde5297cf9");
        _context.Articles.Add(new Article() { Id = id });
        await _context.SaveChangesAsync();

        var repository = new ArticleRepository(_context);

        try
        {
            //Act
            repository.CreateArticle(new Article
            {
                Id = id
            });
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            //Assert
            ex.Should().BeOfType<InvalidOperationException>();
        }
    }

    [Fact]
    public async Task DeleteArticle_ShouldDeleteArticle_WhenEntityContainInDb()
    {
        //Arrange
        Guid id = new("18e1b9a8-9516-4492-9991-5eba76d8d749");
        var article = new Article
        {
            Id = id
        };
        _context.Articles.Add(article);
        await _context.SaveChangesAsync();
        _context.Articles.Should().HaveCount(1);

        var repository = new ArticleRepository(_context);

        //Act
        repository.DeleteArticle(article);
        await _context.SaveChangesAsync();

        //Assert
        _context.Articles.Should().BeEmpty();
    }

    [Fact]
    public async Task DeleteArticle_ShouldThrowException_WhenTryToDeleteUnrealEntity()
    {
        //Arrange
        Guid id = new("18e1b9a8-9516-4492-9991-5eba76d8d749");
        _context.Articles.Add(new Article { Id = id });
        await _context.SaveChangesAsync();
        var repository = new ArticleRepository(_context);

        try
        {
            //Act
            var unexpectedArticle = new Article
            {
                Id = new Guid("cecbdaf6-bf0e-4b91-99ff-e779f417e119")
            };
            repository.DeleteArticle(unexpectedArticle);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            //Assert
            ex.Should().BeOfType<DbUpdateConcurrencyException>();
        }
    }

    [Fact]
    public async Task DeleteUser_ShoulThrowException_WhenUserNoContainInDb()
    {
        //Arrange
        var repository = new ArticleRepository(_context);

        try
        {
            //Act
            var article = new Article
            {
                Id = new Guid("3dad2287-60c0-4cfe-be8a-7d8d6c491d3f"),
            };
            repository.DeleteArticle(article);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            //Assert
            ex.Should().BeOfType<DbUpdateConcurrencyException>();
        }
    }
}