namespace Ukrainian_Culture.Tests.RepositoriesTests;

public class CultureRepositoryTests
{
    private readonly RepositoryContext _context;
    public CultureRepositoryTests()
    {
        using var factory = new ConnectionFactory();
        _context = factory.CreateContextForInMemory(new CultureModel());
    }

    [Fact]
    public async Task GetCultureWithContentAsync_ShouldReturnCultureWithInfo_WhenAllContainsInDb()
    {
        //Arrange
        Guid articleId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid categoryId = new("5b32effd-1111-4cab-8ac9-3258c746aa53");
        Guid cultureId = new("5b32effd-2636-4cab-8ac9-3258c746aa53");

        _context.Cultures.Add(new Culture()
        {
            Id = cultureId,
            Name = "en",
            DisplayedName = "English"
        });

        _context.CategoryLocales.Add(new CategoryLocale()
        {
            CategoryId = categoryId,
            CultureId = cultureId
        });

        _context.ArticlesLocales.Add(new ArticlesLocale()
        {
            Id = articleId,
            CultureId = cultureId
        });
        await _context.SaveChangesAsync();
        var cultureRepository = new CultureRepository(_context);
        //Act

        var result = await cultureRepository.GetCultureWithContentAsync(cultureId, ChangesType.AsNoTracking);
        //Assert
        result.ArticlesTranslates.Should().HaveCount(1);
        result.Categories.Should().HaveCount(1);
        result.Name.Should().Be("en");
    }

    [Fact]
    public async Task GetCultureWithContentAsync_ShouldReturnCultureWithEmptyArticles_WhenArticlesDontContainsInDb()
    {
        //Arrange
        Guid categoryId = new("5b32effd-1111-4cab-8ac9-3258c746aa53");
        Guid cultureId = new("5b32effd-2636-4cab-8ac9-3258c746aa53");

        _context.Cultures.Add(new Culture()
        {
            Id = cultureId,
            Name = "en",
            DisplayedName = "English"
        });

        _context.CategoryLocales.Add(new CategoryLocale()
        {
            CategoryId = categoryId,
            CultureId = cultureId,
        });

        await _context.SaveChangesAsync();
        var cultureRepository = new CultureRepository(_context);
        //Act

        var result = await cultureRepository.GetCultureWithContentAsync(cultureId, ChangesType.AsNoTracking);
        //Assert
        result.ArticlesTranslates.Should().BeEmpty();
        result.Categories.Should().HaveCount(1);
        result.Name.Should().Be("en");
    }
    [Fact]
    public async Task GetCultureWithContentAsync_ShouldReturnCultureWithEmptyCategories_WhenCategoriesDontContainsInDb()
    {
        //Arrange
        Guid articleId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid cultureId = new("5b32effd-2636-4cab-8ac9-3258c746aa53");

        _context.Cultures.Add(new Culture
        {
            Id = cultureId,
            Name = "en",
            DisplayedName = "English"
        });

        _context.ArticlesLocales.Add(new ArticlesLocale
        {
            Id = articleId,
            CultureId = cultureId
        });

        await _context.SaveChangesAsync();
        var cultureRepository = new CultureRepository(_context);
        //Act

        var result = await cultureRepository.GetCultureWithContentAsync(cultureId, ChangesType.AsNoTracking);
        //Assert
        result.ArticlesTranslates.Should().HaveCount(1);
        result.Categories.Should().BeEmpty();
        result.Name.Should().Be("en");
    }

    [Theory]
    [InlineData("5eca5808-4f44-4c4c-b481-72d2bdf24203", "en")]
    [InlineData("5b32effd-2636-4cab-8ac9-3258c746aa53", "ua")]
    public async Task GetCultureWithContentAsync_ShouldReturnCorrectCulture_WhenContainsInDb(string idToCompareAsString, string expectedLanguage)
    {
        //Arrange
        Guid firstCultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid secondCultureId = new("5b32effd-2636-4cab-8ac9-3258c746aa53");

        _context.Cultures.AddRange(
            new List<Culture>
            {
                new()
                {
                    Id = firstCultureId,
                    Name = "en",
                    DisplayedName = "English"
                },
                new ()
                {
                    Id = secondCultureId,
                    Name = "ua",
                    DisplayedName = "Ukraine"
                }
            });

        await _context.SaveChangesAsync();
        var cultureRepository = new CultureRepository(_context);
        //Act
        Guid idToCompare = new(idToCompareAsString);
        var result = await cultureRepository.GetCultureWithContentAsync(idToCompare, ChangesType.AsNoTracking);
        //Assert
        result.Name.Should().Be(expectedLanguage);
    }

    [Fact]
    public async Task GetCultureWithContentAsync_ShouldThrowException_WhenIdNotContainInDb()
    {
        //Arrange
        var cultureRepository = new CultureRepository(_context);
        try
        {
            //Act
            _ = await cultureRepository.GetCultureWithContentAsync(new Guid(), ChangesType.AsNoTracking);
        }
        catch (Exception ex)
        {
            //Assert
            ex.Should().BeOfType<InvalidOperationException>();
        }
    }
}
