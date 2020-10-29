using System;
using AutoMapper;
using Services.Models;

namespace Services.Mapping
{
    public class PersonDtoMapper : Profile
    {
        public PersonDtoMapper()
        {
            CreateMap<Domain.Models.Person, PersonDto>();
        }
    }
}
