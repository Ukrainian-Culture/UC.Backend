using Contracts;
using Entities;
using Entities.Models;
using FluentAssertions;
using Repositories;
using Ukrainian_Culture.Tests.DbModels;

namespace Ukrainian_Culture.Tests;

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
        int cultureId = 1;
        _context.Cultures.Add(new Culture()
        {
            Id = cultureId,
            Name = "en",
            DisplayedName = "English"
        });

        _context.CategoryLocales.Add(new CategoryLocale()
        {
            CategoryId = 1,
            CultureId = cultureId
        });

        _context.ArticlesLocales.Add(new ArticlesLocale()
        {
            Id = 1,
            CultureId = cultureId
        });
        await _context.SaveChangesAsync();
        var cultureRepository = new CultureRepository(_context);
        //Act

        var result = (await cultureRepository.GetCultureWithContentAsync(cultureId, ChangesType.AsNoTracking));
        //Assert
        result.ArticlesTranslates.Should().HaveCount(1);
        result.Categories.Should().HaveCount(1);
        result.Name.Should().Be("en");
    }

    [Fact]
    public async Task GetCultureWithContentAsync_ShouldReturnCultureWithEmptyArticles_WhenArticlesDontContainsInDb()
    {
        //Arrange
        int cultureId = 1;
        _context.Cultures.Add(new Culture()
        {
            Id = cultureId,
            Name = "en",
            DisplayedName = "English"
        });

        _context.CategoryLocales.Add(new CategoryLocale()
        {
            CategoryId = 1,
            CultureId = cultureId,
        });

        await _context.SaveChangesAsync();
        var cultureRepository = new CultureRepository(_context);
        //Act

        var result = (await cultureRepository.GetCultureWithContentAsync(cultureId, ChangesType.AsNoTracking));
        //Assert
        result.ArticlesTranslates.Should().BeEmpty();
        result.Categories.Should().HaveCount(1);
        result.Name.Should().Be("en");
    }
    [Fact]
    public async Task GetCultureWithContentAsync_ShouldReturnCultureWithEmptyCategories_WhenCategoriesDontContainsInDb()
    {
        //Arrange
        int cultureId = 1;
        _context.Cultures.Add(new Culture
        {
            Id = cultureId,
            Name = "en",
            DisplayedName = "English"
        });

        _context.ArticlesLocales.Add(new ArticlesLocale
        {
            Id = 1,
            CultureId = cultureId
        });

        await _context.SaveChangesAsync();
        var cultureRepository = new CultureRepository(_context);
        //Act

        var result = (await cultureRepository.GetCultureWithContentAsync(cultureId, ChangesType.AsNoTracking));
        //Assert
        result.ArticlesTranslates.Should().HaveCount(1);
        result.Categories.Should().BeEmpty();
        result.Name.Should().Be("en");
    }

    [Theory]
    [InlineData(1, "en")]
    [InlineData(2, "ua")]
    public async Task GetCultureWithContentAsync_ShouldReturnCorrectCulture_WhenContainsInDb(int idToCompare, string expectedLanguage)
    {
        //Arrange
        _context.Cultures.AddRange(
            new List<Culture>
            {
                new()
                {
                    Id = 1,
                    Name = "en",
                    DisplayedName = "English"
                },
                new ()
                {
                    Id = 2,
                    Name = "ua",
                    DisplayedName = "Ukraine"
                }
            });

        await _context.SaveChangesAsync();
        var cultureRepository = new CultureRepository(_context);
        //Act

        var result = (await cultureRepository.GetCultureWithContentAsync(idToCompare, ChangesType.AsNoTracking));
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
            var result = (await cultureRepository.GetCultureWithContentAsync(2, ChangesType.AsNoTracking));
        }
        catch (Exception ex)
        {
            //Assert
            ex.Should().BeOfType<InvalidOperationException>();
        }
    }
}
