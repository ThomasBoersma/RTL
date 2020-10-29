using System;
using System.Collections.Generic;
using Services.Models;
using Domain.Repositories;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using Common.Paging;

namespace Services.Services
{
    public class ShowService : IShowService
    {
        private readonly IMapper mapper;
        private readonly IShowRepository showRepository;

        public ShowService(IMapper mapper, IShowRepository showRepository)
        {
            this.mapper = mapper;
            this.showRepository = showRepository;
        }

        public async Task<IList<ShowDto>> GetAsync(PagingOptions pagingOptions)
        {
            var shows = await showRepository.GetAsync(pagingOptions);

            shows.ForEach(show => show.Cast = show.Cast.OrderByDescending(x => x.Birthday).ToList());

            return mapper.Map<IList<ShowDto>>(shows);
        }
    }
}
