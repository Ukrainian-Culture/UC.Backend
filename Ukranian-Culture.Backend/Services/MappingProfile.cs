using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace Ukranian_Culture.Backend.Services;

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
            .ForMember(artT => artT.Category,
                opt => opt.MapFrom(s
                    => s.article.Category.Name));

        CreateMap<(Article article, ArticlesLocale articlesLocale), HistoryDto>()
            .ForMember(x => x.ShortDescription, opt => opt.MapFrom(s => s.articlesLocale.ShortDescription))
            .ForMember(x => x.Date, opt => opt.MapFrom(s => s.article.Date.ToString("MM.dd.yyyy")))
            .ForMember(x => x.ActicleId, opt => opt.MapFrom(s => s.article.Id))
            .ForMember(x => x.Region, opt => opt.MapFrom(s => s.article.Region));

        CreateMap<Article, ArticleToGetDto>()
            .ForMember(artDto => artDto.Date,
                opt => opt.MapFrom(art => art.Date.ToString("mm.dd.yyyy")));
        CreateMap<ArticleToCreateDto, Article>();
        CreateMap<ArticleToUpdateDto, Article>()
            .ForMember(art => art.CategoryId,
                opt => opt.MapFrom(artToUpd => artToUpd.CategoryId))
            .ForMember(art => art.Region,
                opt => opt.MapFrom(artToUpd => artToUpd.Region))
            .ForMember(art => art.Date,
                opt => opt.MapFrom(artToUpd => artToUpd.Date))
            .ForMember(art => art.Id,
                opt => opt.Ignore())
            .ForMember(art => art.Category,
                opt => opt.Ignore());

        CreateMap<ArticlesLocale, ArticlesLocaleToGetDto>()
            .ForMember(artLDto => artLDto.ArticleId,
                opt => opt.MapFrom(artL => artL.Id));
        CreateMap<ArticleLocaleToCreateDto, ArticlesLocale>();
        CreateMap<ArticleLocaleToUpdateDto, ArticlesLocale>()
            .ForMember(art => art.SubText,
                opt => opt.MapFrom(artToUpd => artToUpd.SubText))
            .ForMember(art => art.Title,
                opt => opt.MapFrom(artToUpd => artToUpd.Title))
            .ForMember(art => art.ShortDescription,
                opt => opt.MapFrom(artToUpd => artToUpd.ShortDescription))
            .ForMember(art => art.Content,
                opt => opt.MapFrom(artToUpd => artToUpd.Content))
            .ForMember(art => art.Id,
                opt => opt.Ignore())
            .ForMember(art => art.CultureId,
                opt => opt.Ignore())
            .ForMember(art => art.Culture,
                opt => opt.Ignore());

        CreateMap<Category, CategoryToGetDto>();

        CreateMap<Culture, CultureToGetDto>();
        CreateMap<CultureToCreateDto, Culture>();
        CreateMap<CultureToUpdateDto, Culture>()
            .ForMember(cul => cul.DisplayedName,
                opt => opt.MapFrom(culDto => culDto.DisplayedName))
            .ForMember(cul => cul.Name,
                opt => opt.MapFrom(culDto => culDto.Name))
            .ForMember(cul => cul.Id,
                opt => opt.Ignore())
            .ForMember(cul => cul.ArticlesTranslates,
                opt => opt.Ignore());

        CreateMap<HistoryToCreateDto, UserHistory>()
            .ForMember(his => his.DateOfWatch,
                opt => opt.MapFrom(_ => DateTime.Now))
            .ForMember(his => his.UserId,
                opt => opt.Ignore())
            .ForMember(his => his.Id,
                opt => opt.Ignore());

        CreateMap<UserHistory, UserHistoryToGetDto>()
            .ForMember(hisDto => hisDto.DateOfWatch,
                opt
                    => opt.MapFrom(his => his.DateOfWatch.ToString("MM.dd.yyyy")));
    }
}