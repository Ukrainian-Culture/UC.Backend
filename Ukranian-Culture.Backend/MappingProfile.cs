using AutoMapper;
using Entities.DTOs;
using Entities.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<(Article article, ArticlesLocale articlesLocale), ArticleTileDto>()
            .ForMember(artT => artT.ArticleId,
                opt => opt.MapFrom(s => s.article.Id))
            .ForMember(artT => artT.Region,
                opt => opt.MapFrom(s => s.article.Region))
            .ForMember(artT => artT.SubText,
                opt => opt.MapFrom(s => s.articlesLocale.SubText))
            .ForMember(artT => artT.Title,
                opt => opt.MapFrom(s => s.articlesLocale.Title))
            .ForMember(artT => artT.Type,
                opt => opt.MapFrom(s => s.article.Type))
            .ForMember(artT => artT.Category,
                opt => opt.MapFrom(s
                    => s.articlesLocale.Culture.Categories.First(cat => cat.CategoryId == s.article.CategoryId).Name));

        CreateMap<(Article article, ArticlesLocale articlesLocale), HistoryDto>()
              .ForMember(x => x.ShortDescription, opt => opt.MapFrom(s => s.articlesLocale.ShortDescription))
              .ForMember(x => x.Date, opt => opt.MapFrom(s => s.article.Date.ToString("MM.dd.yyyy")))
              .ForMember(x => x.ActicleId, opt => opt.MapFrom(s => s.article.Id))
              .ForMember(x => x.Region, opt => opt.MapFrom(s => s.article.Region));

        CreateMap<ArticleToCreateDto, Article>();
        CreateMap<ArticleToUpdateDto, Article>()
            .ForMember(art => art.CategoryId,
                opt => opt.MapFrom(artToUpd => artToUpd.CategoryId))
            .ForMember(art => art.Region,
                opt => opt.MapFrom(artToUpd => artToUpd.Region))
            .ForMember(art => art.Date,
                opt => opt.MapFrom(artToUpd => artToUpd.Date))
            .ForMember(art => art.Type,
                opt => opt.MapFrom(artToUpd => artToUpd.Type))
            .ForMember(art => art.Id,
                opt => opt.Ignore())
            .ForMember(art => art.Category,
                opt => opt.Ignore());
    }
}
