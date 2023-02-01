using HtmlAgilityPack;
using Ukrainian_Culture.Tests.RepositoriesTests;

namespace Parsers.Tests;
public class ParsersTests
{

    private readonly IParser _parser = Substitute.For<IParser>();

    [Fact]
    public async void GetNodeByUrl_ShouldBeEmpty_WhenHaveNoUrlAndNoSelectNodeString()
    {
        _parser
            .GetNodeByUrl("", "")
            .Returns(new HtmlNodeCollection(new HtmlNode(HtmlNodeType.Document, new HtmlDocument(), 0)));

        var result = (await _parser.GetNodeByUrl("", "")).ToList();

        result.Should().HaveCount(0);
    }

    [Fact]
    public async void GetNodeByUrl_ShouldReturnCorrectAmountOfChildNodes_WhenHaveUrlAndNoSelectNodeString()
    {
        var html = @"<!DOCTYPE html>
            <html>
                <body>
                    <h1>Learn To Code in C#</h1>
                    <p>Programming is really <i>easy</i>.</p>
                </body>
            </html>";

        var dom = new HtmlDocument();
        dom.LoadHtml(html);

        var node = dom.DocumentNode.OwnerDocument.DocumentNode.ChildNodes;

        _parser
            .GetNodeByUrl("url", "")
            .Returns(node);

        var result = (await _parser.GetNodeByUrl("url", "")).ToList();

        result.Should().HaveCount(3);
        result[0].Name.Should().Be("#comment");
        result[1].Name.Should().Be("#text");
        result[2].Name.Should().Be("html");
    }

    [Fact]
    public async void GetNodeByUrl_ShouldReturnCorrectHtmlNodes_WhenHaveUrlAndSelectNodeString()
    {
        var html = @"<!DOCTYPE html>
            <html>
                <body>
                    <h1>Learn To Code in C#</h1>
                    <p>Programming is really <i>easy</i>.</p>
                </body>
            </html>";

        var dom = new HtmlDocument();
        dom.LoadHtml(html);

        var node = dom.DocumentNode.OwnerDocument.DocumentNode.ChildNodes[2].ChildNodes[1].ChildNodes;

        _parser
           .GetNodeByUrl("url", "//body")
           .Returns(node);

        var result = (await _parser.GetNodeByUrl("url", "//body")).ToList();

        result.Should().HaveCount(5);
        result[0].Name.Should().Be("#text");
        result[1].Name.Should().Be("h1");
        result[2].Name.Should().Be("#text");
        result[3].Name.Should().Be("p");
        result[4].Name.Should().Be("#text");
    }
}