namespace Ukrainian_Culture.Tests.RepositoriesTests;

public class CategoryLocalesRepositoryTests
{
    private readonly RepositoryContext _context;

    public CategoryLocalesRepositoryTests()
    {
        using var factory = new ConnectionFactory();
        _context = factory.CreateContextForInMemory(new CategoryLocaleModel());
    }

    [Fact]
    public async Task GetAllByConditionAsync_SholdReturnEmptyCollection_WhenExpressionIsEqualToFalseAndDbIsNotEmpty()
    {
        //Arrange
        _context.CategoryLocales.Add(new CategoryLocale());
        await _context.SaveChangesAsync();
        var categoryLocaleRepository = new CategoryLocalesRepository(_context);
        //Act
        var result = await categoryLocaleRepository.GetAllByConditionAsync(_ => false, ChangesType.AsNoTracking);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllByConditionAsync_SholdReturnAllCollection_WhenExpressionIsEqualToTrueAndDbIsNotEmpty()
    {
        //Arrange
        _context.CategoryLocales.AddRange(new List<CategoryLocale>()
        {
            new() {CategoryId = Guid.NewGuid()},
            new() {CategoryId = Guid.NewGuid()}
        });
        await _context.SaveChangesAsync();
        var categoryLocaleRepository = new CategoryLocalesRepository(_context);
        //Act
        var result = await categoryLocaleRepository.GetAllByConditionAsync(_ => true, ChangesType.AsNoTracking);

        //Assert
        result.Should().HaveCount(2);
    }


    public static IEnumerable<object[]> TestData()
    {
        Guid englishCultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid ukrainianCultureId = new("5b32effd-1111-4cab-8ac9-3258c746aa53");
        Guid categoryId = new("5b32effd-2636-4cab-8ac9-3258c746aa53");

        yield return new object[]
        {
            new CategoryLocale()
            {
                CategoryId = categoryId,
                CultureId = englishCultureId,
                Name = "people"
            },
            englishCultureId
        };

        yield return new object[]
        {
            new CategoryLocale()
            {
                CategoryId = categoryId,
                CultureId = ukrainianCultureId,
                Name = "люди"
            },
            ukrainianCultureId
        };
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public async Task GetAllByConditionAsync_SholdReturnPartOfCollection_WhenHasCorrectExpressionAndDbIsNotEmpty(
        CategoryLocale expected, Guid idToCompare)
    {
        //Arrange
        Guid englishCultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid ukrainianCultureId = new("5b32effd-1111-4cab-8ac9-3258c746aa53");
        Guid categoryId = new("5b32effd-2636-4cab-8ac9-3258c746aa53");

        _context.CategoryLocales.AddRange(new List<CategoryLocale>()
            {
                new()
                {
                    CategoryId = categoryId,
                    CultureId = englishCultureId,
                    Name = "people"
                },
                new()
                {
                    CategoryId = categoryId,
                    CultureId = ukrainianCultureId,
                    Name = "люди"
                }
            }
        );
        await _context.SaveChangesAsync();

        var articleRepository = new CategoryLocalesRepository(_context);

        //Act
        var article
            = (await articleRepository
                .GetAllByConditionAsync(art => art.CultureId == idToCompare, ChangesType.AsNoTracking)).ToList();

        //Assert

        article.Should().HaveCount(1);
        article[0].Should()
            .Match<CategoryLocale>(category => category.CategoryId == expected.CategoryId &&
                                               category.CultureId == expected.CultureId &&
                                               category.Name == expected.Name);
    }

    public static IEnumerable<object[]> GetFirstByConditionTestData()
    {
        Guid englishCultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid categoryId = new("5b32effd-2636-4cab-8ac9-3258c746aa53");

        yield return new object[]
        {
            new CategoryLocale()
            {
                CategoryId = categoryId,
                CultureId = englishCultureId,
                Name = "people"
            },
            englishCultureId, categoryId
        };

        yield return new object[]
        {
            null, Guid.Empty, Guid.Empty
        };
    }

    [Theory]
    [MemberData(nameof(GetFirstByConditionTestData))]
    public async Task GetFirstByCondition_ShouldReturnExpected(CategoryLocale expected, Guid cultureId, Guid id)
    {
        //Arrange
        Guid englishCultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid categoryId = new("5b32effd-2636-4cab-8ac9-3258c746aa53");

        _context.CategoryLocales.AddRange(new List<CategoryLocale>
            {
                new()
                {
                    CategoryId = categoryId,
                    CultureId = englishCultureId,
                    Name = "people"
                }
            }
        );
        await _context.SaveChangesAsync();

        var categoryLocalesRepository = new CategoryLocalesRepository(_context);

        //Act
        var categoryLocale
            = await categoryLocalesRepository
                .GetFirstByCondition(art => art.CultureId == cultureId && art.CategoryId == id,
                    ChangesType.AsNoTracking);

        //Assert
        categoryLocale.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetFirstByCondition_ShouldThrowException_WhenConditionIsNull()
    {
        //Arrange
        var repository = new CategoryLocalesRepository(_context);
        try
        {
            //Act
            await repository.GetFirstByCondition(null, ChangesType.AsNoTracking);
        }
        catch (Exception e)
        {
            //Assert
            e.Should().BeOfType<ArgumentNullException>();
        }
    }
}