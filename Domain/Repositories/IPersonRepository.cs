using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Models;

namespace Domain.Repositories
{
    public interface IPersonRepository
    {
        public Task AddAsync(Person person);
        public Task<List<Person>> GetAsync();
        public Task<Person> GetAsync(int id);
    }
}
