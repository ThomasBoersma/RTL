using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Domain.Models;
using Common.Paging;

namespace Domain.Repositories
{
    public class ShowRepository : Repository, IShowRepository
    {
        public ShowRepository(SQLServerDBContext context) : base(context)
        {

        }

        public async Task AddAsync(Show show)
        {
            await context.Shows.AddAsync(show);
            await context.SaveChangesAsync();
        }

        public async Task<Show> GetAsync(int id)
        {
            var results = await context.Shows.Include(x => x.Cast).SingleOrDefaultAsync(x => x.Id.Equals(id));
            return results;
        }

        public async Task<List<Show>> GetAsync(PagingOptions pagingOptions)
        {
            var results = await context.Shows
                .Include(x => x.Cast)
                .Skip(pagingOptions.Skip)
                .Take(pagingOptions.Take)
                .ToListAsync();
            return results;
        }
    }
}
