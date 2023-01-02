using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace Ukranian_Culture.Backend
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tuple<Article,ArticlesLocale>,HistoryDto>();
        }
    }
}
