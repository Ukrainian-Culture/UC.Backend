using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace Ukranian_Culture.Backend;

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
    }
}