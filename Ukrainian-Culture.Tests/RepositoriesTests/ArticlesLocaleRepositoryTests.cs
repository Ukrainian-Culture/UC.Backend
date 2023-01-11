namespace Ukrainian_Culture.Tests.RepositoriesTests;

public class ArticlesLocaleRepositoryTests
{
    private readonly RepositoryContext _context;
    public ArticlesLocaleRepositoryTests()
    {
        using var factory = new ConnectionFactory();
        _context = factory.CreateContextForInMemory(new ArticlesLocaleModel());
    }

    public static IEnumerable<object[]> TestData()
    {
        Guid articleId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid firstCultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24111");
        Guid secondCultureId = new("5b32effd-2636-4cab-8ac9-3258c746aa53");

        yield return new object[]
        {
            new ArticlesLocale
            {
                Id = articleId,
                CultureId = firstCultureId,
                Title = "About Bohdan Khmelnytsky",
                Content = "About Bohdan Khmelnytsky .... ",
                SubText = "About Bohdan Khmelnytsky",
                ShortDescription = "About Bohdan Khmelnytsky"
            }, firstCultureId
        };

        yield return new object[]
        {
            new ArticlesLocale
            {
                Id = articleId,
                CultureId = secondCultureId,
                Title = "Про Богдана Хмельницького",
                Content = "Про Богдана Хмельницького .... ",
                SubText = "Про Богдана Хмельницького",
                ShortDescription = "Про Богдана Хмельницького"
            }, secondCultureId
        };
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public async Task GetArticlesLocaleByConditionAsync_SholdReturnCollectionOfAllElements_WhenExpressionIsEqualToTrueAndDbIsNotEmpty(ArticlesLocale expected)
    {
        //Arrange
        Guid articleId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid testableCultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24111");

        _context.ArticlesLocales.AddRange(new List<ArticlesLocale>()
            {
                new ArticlesLocale
                {
                    Id = articleId,
                    CultureId = testableCultureId,
                    Title = "About Bohdan Khmelnytsky",
                    Content = "About Bohdan Khmelnytsky .... ",
                    SubText = "About Bohdan Khmelnytsky",
                    ShortDescription = "About Bohdan Khmelnytsky"
                },
                new ArticlesLocale
                {
                    Id = articleId,
                    CultureId = new("5b32effd-2636-4cab-8ac9-3258c746aa53"),
                    Title = "Про Богдана Хмельницького",
                    Content = "Про Богдана Хмельницького .... ",
                    SubText = "Про Богдана Хмельницького",
                    ShortDescription = "Про Богдана Хмельницького"
                }
            }
        );

        _context.Cultures.Add(new Culture { Id = testableCultureId });
        await _context.SaveChangesAsync();

        var articleRepository = new ArticleLocalesRepository(_context);

        //Act
        var artclesLocale = (await articleRepository.GetArticlesLocaleByConditionAsync(_ => true, ChangesType.AsNoTracking))
            .ToList();

        //Assert
        artclesLocale.Should().HaveCount(2);
        artclesLocale.Should().ContainEquivalentOf(expected);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public async Task GetArticlesLocaleByConditionAsync_SholdReturnCollectionOfNotAllElements_WhenHasCorrectExpressionAndDbIsNotEmpty(ArticlesLocale expected, Guid idExpected)
    {
        //Arrange
        Guid articleId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid firstCultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24111");
        Guid secondCultureId = new("5b32effd-2636-4cab-8ac9-3258c746aa53");

        _context.ArticlesLocales.AddRange(new List<ArticlesLocale>()
            {
                new ()
                {
                    Id = articleId,
                    CultureId = firstCultureId,
                    Title = "About Bohdan Khmelnytsky",
                    Content = "About Bohdan Khmelnytsky .... ",
                    SubText = "About Bohdan Khmelnytsky",
                    ShortDescription = "About Bohdan Khmelnytsky"
                },
                new()
                {
                    Id = articleId,
                    CultureId = secondCultureId,
                    Title = "Про Богдана Хмельницького",
                    Content = "Про Богдана Хмельницького .... ",
                    SubText = "Про Богдана Хмельницького",
                    ShortDescription = "Про Богдана Хмельницького"
                }
            }
        );

        _context.Cultures.Add(new Culture { Id = firstCultureId });
        await _context.SaveChangesAsync();

        var articleRepository = new ArticleLocalesRepository(_context);

        //Act
        var article = (await articleRepository.GetArticlesLocaleByConditionAsync(art => art.CultureId == idExpected, ChangesType.AsNoTracking))
            .ToList();

        //Assert
        article.Should().HaveCount(1);

        article[0].Should()
            .Match<ArticlesLocale>(art => art.Id == expected.Id &&
                                   art.CultureId == expected.CultureId &&
                                   art.Title == expected.Title &&
                                   art.Content == expected.Content &&
                                   art.SubText == expected.SubText &&
                                   art.ShortDescription == expected.ShortDescription
                                   );
    }

    [Fact]
    public async Task GetArticlesLocaleByConditionAsync_SholdReturnEmptyCollection_WhenHasIncorrectExpressionAndDbIsNotEmpty()
    {
        //Arrange
        Guid articleId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid firstCultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24111");
        Guid secondCultureId = new("5b32effd-2636-4cab-8ac9-3258c746aa53");

        _context.ArticlesLocales.AddRange(new List<ArticlesLocale>()
            {
                new ()
                {
                    Id = articleId,
                    CultureId = firstCultureId,
                    Title = "About Bohdan Khmelnytsky",
                    Content = "About Bohdan Khmelnytsky .... ",
                    SubText = "About Bohdan Khmelnytsky",
                    ShortDescription = "About Bohdan Khmelnytsky"
                },
                new()
                {
                    Id = articleId,
                    CultureId = secondCultureId,
                    Title = "Про Богдана Хмельницького",
                    Content = "Про Богдана Хмельницького .... ",
                    SubText = "Про Богдана Хмельницького",
                    ShortDescription = "Про Богдана Хмельницького"
                }
            });

        _context.Cultures.Add(new Culture { Id = firstCultureId });
        await _context.SaveChangesAsync();

        var articleRepository = new ArticleLocalesRepository(_context);

        Guid idUnexpected = new("11111111-1111-1111-1111-111111111111");

        //Act
        var articleslocale = (await articleRepository.GetArticlesLocaleByConditionAsync(art => art.CultureId == idUnexpected, ChangesType.AsNoTracking))
            .ToList();

        //Assert
        articleslocale.Should().BeEmpty();
    }

    [Fact]
    public async Task GetArticlesLocaleByConditionAsync_SholdReturnEmptyCollection_WhenDbIsEmpty()
    {
        //Arrange
        _context.ArticlesLocales.AddRange(Enumerable.Empty<ArticlesLocale>());

        var articleRepository = new ArticleLocalesRepository(_context);

        //Act
        Guid idExpected = new("11111111-1111-1111-1111-111111111111");
        var article = (await articleRepository.GetArticlesLocaleByConditionAsync(art => art.Id == idExpected, ChangesType.AsNoTracking))
            .ToList();

        //Assert
        article.Should().BeEmpty();
    }
}