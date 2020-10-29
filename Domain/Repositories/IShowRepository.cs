using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Models;
using Common.Paging;

namespace Domain.Repositories
{
    public interface IShowRepository
    {
        public Task AddAsync(Show show);
        public Task<List<Show>> GetAsync(PagingOptions pagingOptions);
        public Task<Show> GetAsync(int id);
    }
}
