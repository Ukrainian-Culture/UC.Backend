using Entities.SearchEngines;

namespace Ukrainian_Culture.Tests.ServicesTests;

public class ArticleTileSearchEngineTests
{
    [Fact]
    public void AddArticlesTileToIndex_ShouldThrowException_WhenRecievesNull()
    {
        //Arrange
        var engine = new ArticleTileSearchEngine();
        try
        {
            //Act
            engine.AddArticlesTileToIndex(null);
        }
        catch (Exception e)
        {
            //Assert
            e.Should().BeOfType<NullReferenceException>();
        }
    }

    public static IEnumerable<object[]> TestData()
    {
        var testableTitleEntity = new ArticleTileDto
        {
            ArticleId = new Guid("ffdaaf68-5e75-4342-bb10-24e2019d045d"),
            Category = "-",
            Region = "-",
            Title = "Title",
            SubText = "-"
        };
        var testableRegionEntity = new ArticleTileDto
        {
            ArticleId = new Guid("ffdaaf68-1111-4342-bb10-24e2019d045d"),
            Category = "-",
            Region = "Region",
            Title = "-",
            SubText = "-"
        };
        var testableSubTextEntity = new ArticleTileDto
        {
            ArticleId = new Guid("ffdaaf68-1111-4342-bb10-24e2019d045d"),
            Category = "-",
            Region = "-",
            Title = "-",
            SubText = "SubText"
        };
        var list = new List<ArticleTileDto> { testableRegionEntity, testableTitleEntity, testableSubTextEntity };

        yield return new object[]
        {
            list, "Title", new List<ArticleTileDto> { testableTitleEntity }
        };
        yield return new object[]
        {
            list, "Ti", new List<ArticleTileDto> { testableTitleEntity }
        };
        yield return new object[]
        {
            list, "Tidle", new List<ArticleTileDto> { testableTitleEntity }
        };
        yield return new object[]
        {
            list, "Region", new List<ArticleTileDto> { testableRegionEntity }
        };
        yield return new object[]
        {
            list, "Re", new List<ArticleTileDto> { testableRegionEntity }
        };
        yield return new object[]
        {
            list, "Redion", new List<ArticleTileDto> { testableRegionEntity }
        };
        yield return new object[]
        {
            list, "SubText", new List<ArticleTileDto> { testableSubTextEntity }
        };
        yield return new object[]
        {
            list, "Sub", new List<ArticleTileDto> { testableSubTextEntity }
        };
        yield return new object[]
        {
            list, "SubTexd", new List<ArticleTileDto> { testableSubTextEntity }
        };
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void Search_ShouldReturnCorrectEntities_WhenRecievedQuery(
        IEnumerable<ArticleTileDto> testList, string query, IEnumerable<ArticleTileDto> expectedList)
    {
        //Arrange
        var engine = new ArticleTileSearchEngine();
        //Act
        engine.AddArticlesTileToIndex(testList);
        var result = engine.Search(query).ToList();
        //Assert
        result.Should().HaveCount(1);
        result.Should().BeEquivalentTo(expectedList);
    }
}