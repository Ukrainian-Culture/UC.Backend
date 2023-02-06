using Entities.DTOs;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;

namespace Entities.SearchEngines;

public class ArticleTileSearchEngine
{
    private readonly StandardAnalyzer _analyzer;
    private readonly RAMDirectory _directory;
    private readonly IndexWriter _writer;
    private const LuceneVersion Version = LuceneVersion.LUCENE_48;

    public ArticleTileSearchEngine()
    {
        _analyzer = new StandardAnalyzer(Version);
        _directory = new RAMDirectory();
        var config = new IndexWriterConfig(Version, _analyzer);
        _writer = new IndexWriter(_directory, config);
    }

    public void AddArticlesTileToIndex(IEnumerable<ArticleTileDto> articles)
    {
        foreach (var article in articles)
        {
            var document = new Document
            {
                new StringField("ArticleId", article.ArticleId.ToString(), Field.Store.YES),
                new TextField("Region", article.Region, Field.Store.YES),
                new TextField("SubText", article.SubText, Field.Store.YES),
                new TextField("Title", article.Title, Field.Store.YES),
                new TextField("Category", article.Category, Field.Store.YES)
            };
            _writer.AddDocument(document);
        }

        _writer.Commit();
    }

    public IEnumerable<ArticleTileDto> Search(string searchTerm)
    {
        var indexSearch = new IndexSearcher(DirectoryReader.Open(_directory));
        var hits = indexSearch.Search(GetQuery(searchTerm), 10).ScoreDocs;
        return GetEntitiesFromHits(hits, indexSearch);
    }

    private Query GetQuery(string searchTerm)
    {
        string[] fields = { "SubText", "Region", "Title" };
        var queryParser = new MultiFieldQueryParser(Version, fields, _analyzer);
        var searchQuery = string.Join("~ ", searchTerm.Split(" ", StringSplitOptions.RemoveEmptyEntries)) + "~";
        return queryParser.Parse($"{searchQuery} OR {searchQuery.Replace("~", "*")}");
    }

    private static IEnumerable<ArticleTileDto> GetEntitiesFromHits(IEnumerable<ScoreDoc> hits,
        IndexSearcher indexSearch)
        => hits
            .Select(hit => indexSearch.Doc(hit.Doc))
            .Select(document => new ArticleTileDto
            {
                ArticleId = new Guid(document.Get("ArticleId")),
                Category = document.Get("Category"),
                SubText = document.Get("SubText"),
                Title = document.Get("Title"),
                Region = document.Get("Region")
            }).ToList();
}