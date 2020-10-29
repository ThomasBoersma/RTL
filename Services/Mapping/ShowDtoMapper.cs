using AutoMapper;
using Services.Models;

namespace Services.Mapping
{
    public class ShowDtoMapper : Profile
    {
        public ShowDtoMapper()
        {
            CreateMap<Domain.Models.Show, ShowDto>();
        }
    }
}