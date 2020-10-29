using System;
using System.Collections.Generic;
using Services.Models;
using System.Threading.Tasks;
using Common.Paging;

namespace Services.Services
{
    public interface IShowService
    {
        public Task<IList<ShowDto>> GetAsync(PagingOptions pagingOptions);
    }
}
