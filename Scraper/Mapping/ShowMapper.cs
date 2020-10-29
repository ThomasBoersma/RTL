using AutoMapper;
using Scraper.Models;

namespace Scraper.Mapping
{
    public class ShowMapper : Profile
    {
        public ShowMapper()
        {
            CreateMap<Show, Domain.Models.Show>()
                .ForMember(dest => dest.TvMazeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
