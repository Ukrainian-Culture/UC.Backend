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
        yield return new object[]
        {
            new ArticlesLocale
            {
                Id = 1,
                CultureId = 1,
                Title = "About Bohdan Khmelnytsky",
                Content = "About Bohdan Khmelnytsky .... ",
                SubText = "About Bohdan Khmelnytsky",
                ShortDescription = "About Bohdan Khmelnytsky"
            }, 1
        };

        yield return new object[]
        {
            new ArticlesLocale
            {
                Id = 1,
                CultureId = 2,
                Title = "Про Богдана Хмельницького",
                Content = "Про Богдана Хмельницького .... ",
                SubText = "Про Богдана Хмельницького",
                ShortDescription = "Про Богдана Хмельницького"
            }, 2
        };
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public async Task GetArticlesLocaleByConditionAsync_SholdReturnCollectionOfAllElements_WhenExpressionIsEqualToTrueAndDbIsNotEmpty(ArticlesLocale expected, int IdExpected)
    {
        //Arrange
        _context.ArticlesLocales.AddRange(new List<ArticlesLocale>()
            {
                new ()
                {
                    Id = 1,
                    CultureId = 1,
                    Title = "About Bohdan Khmelnytsky",
                    Content = "About Bohdan Khmelnytsky .... ",
                    SubText = "About Bohdan Khmelnytsky",
                    ShortDescription = "About Bohdan Khmelnytsky"
                },
                new()
                {
                    Id = 1,
                    CultureId = 2,
                    Title = "Про Богдана Хмельницького",
                    Content = "Про Богдана Хмельницького .... ",
                    SubText = "Про Богдана Хмельницького",
                    ShortDescription = "Про Богдана Хмельницького"
                }
            }
        );

        _context.Cultures.Add(new Culture { Id = 1 });
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
    public async Task GetArticlesLocaleByConditionAsync_SholdReturnCollectionOfNotAllElements_WhenHasCorrectExpressionAndDbIsNotEmpty(ArticlesLocale expected, int IdExpected)
    {
        //Arrange
        _context.ArticlesLocales.AddRange(new List<ArticlesLocale>()
            {
                new ()
                {
                    Id = 1,
                    CultureId = 1,
                    Title = "About Bohdan Khmelnytsky",
                    Content = "About Bohdan Khmelnytsky .... ",
                    SubText = "About Bohdan Khmelnytsky",
                    ShortDescription = "About Bohdan Khmelnytsky"
                },
                new()
                {
                    Id = 1,
                    CultureId = 2,
                    Title = "Про Богдана Хмельницького",
                    Content = "Про Богдана Хмельницького .... ",
                    SubText = "Про Богдана Хмельницького",
                    ShortDescription = "Про Богдана Хмельницького"
                }
            }
        );

        _context.Cultures.Add(new Culture { Id = 1 });
        await _context.SaveChangesAsync();

        var articleRepository = new ArticleLocalesRepository(_context);

        //Act
        var article = (await articleRepository.GetArticlesLocaleByConditionAsync(art => art.CultureId == IdExpected, ChangesType.AsNoTracking))
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
        _context.ArticlesLocales.AddRange(new List<ArticlesLocale>()
            {
                new ()
                {
                    Id = 1,
                    CultureId = 1,
                    Title = "About Bohdan Khmelnytsky",
                    Content = "About Bohdan Khmelnytsky .... ",
                    SubText = "About Bohdan Khmelnytsky",
                    ShortDescription = "About Bohdan Khmelnytsky"
                },
                new()
                {
                    Id = 1,
                    CultureId = 2,
                    Title = "Про Богдана Хмельницького",
                    Content = "Про Богдана Хмельницького .... ",
                    SubText = "Про Богдана Хмельницького",
                    ShortDescription = "Про Богдана Хмельницького"
                }
            }
       );

        _context.Cultures.Add(new Culture { Id = 1 });
        await _context.SaveChangesAsync();

        var articleRepository = new ArticleLocalesRepository(_context);

        var IdUnexpected = 3;

        //Act
        var articleslocale = (await articleRepository.GetArticlesLocaleByConditionAsync(art => art.CultureId == IdUnexpected, ChangesType.AsNoTracking))
            .ToList();

        //Assert
        articleslocale.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public async Task GetArticlesLocaleByConditionAsync_SholdReturnEmptyCollection_WhenDbIsEmpty(ArticlesLocale expected, int IdExpected)
    {
        //Arrange
        _context.ArticlesLocales.AddRange(new List<ArticlesLocale>());

        var articleRepository = new ArticleLocalesRepository(_context);

        //Act
        var article = (await articleRepository.GetArticlesLocaleByConditionAsync(art => art.Id == IdExpected, ChangesType.AsNoTracking))
            .ToList();

        //Assert
        article.Should().BeEmpty();
    }
}