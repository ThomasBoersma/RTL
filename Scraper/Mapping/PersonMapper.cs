using AutoMapper;
using Scraper.Models;

namespace Scraper.Mapping
{
    public class PersonMapper : Profile
    {
        public PersonMapper()
        {
            CreateMap<Person, Domain.Models.Person>()
                .ForMember(dest => dest.TvMazeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
