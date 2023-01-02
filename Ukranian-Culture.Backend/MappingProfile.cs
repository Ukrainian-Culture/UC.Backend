using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace Ukranian_Culture.Backend
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Article, HistoryDto>().ForMember(x => x.SubText, opt => opt.Ignore());
            CreateMap<ArticlesLocale, HistoryDto>()
                .ForMember(x => x.Date, opt => opt.Ignore())
                .ForMember(x => x.ActicleId, opt => opt.Ignore())
                .ForMember(x => x.Region, opt => opt.Ignore());
        }
    }
}
