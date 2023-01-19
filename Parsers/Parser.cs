using HtmlAgilityPack;

namespace Parsers;
public class Parser : IParser
{
    public async Task<HtmlNodeCollection> GetNodeByUrl(string _url, string _node)
    {
        var html = _url;

        HtmlWeb web = new HtmlWeb();

        var htmlDoc = web.Load(html);

        var node = htmlDoc.DocumentNode.SelectNodes(_node);

        return node;
    }
}