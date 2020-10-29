using Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Domain
{
    public class SQLServerDBContext : DbContext
    {
        public SQLServerDBContext()
        {

        }

        public SQLServerDBContext(DbContextOptions<SQLServerDBContext> options) : base(options)
        {

        }

        public DbSet<Show> Shows { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer($"Server=localhost;Database=RTL;User Id=sa;Password=Holl@nd123");
            }
        }
    }
}
