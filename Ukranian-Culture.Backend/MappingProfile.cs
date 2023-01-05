using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace Ukranian_Culture.Backend
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<(Article article, ArticlesLocale articlesLocale), HistoryDto>()
                .ForMember(x=> x.ShortDesc, opt => opt.MapFrom(s => s.articlesLocale.ShortDescription))
                .ForMember(x => x.Date, opt => opt.MapFrom(s => s.article.Date.ToString("MM.dd.yyyy")))
                .ForMember(x => x.ActicleId, opt => opt.MapFrom(s => s.article.Id))
                .ForMember(x => x.Region, opt => opt.MapFrom(s => s.article.Region));
        }
    }
}
