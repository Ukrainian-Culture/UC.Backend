using HtmlAgilityPack;

public interface IParser
{
    Task<HtmlNodeCollection> GetNodeByUrl(string _url, string _node);
}

