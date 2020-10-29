using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Domain.Models;

namespace Domain.Repositories
{
    public class PersonRepository : Repository, IPersonRepository
    {
        public PersonRepository(SQLServerDBContext context) : base (context)
        {

        }

        public async Task AddAsync(Person person)
        {
            await context.Persons.AddAsync(person);
            await context.SaveChangesAsync();
        }

        public async Task<Person> GetAsync(int id)
        {
            var results = await context.Persons.SingleOrDefaultAsync(x => x.Id.Equals(id));
            return results;
        }

        public async Task<List<Person>> GetAsync()
        {
            var results = await context.Persons.ToListAsync();
            return results;
        }

    }
}
