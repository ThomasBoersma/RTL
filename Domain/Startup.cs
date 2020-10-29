using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain.Repositories;

namespace Domain
{
    public class Startup
    {
        public static void Configure(IServiceProvider serviceProvider)
        {
            serviceProvider.GetService<SQLServerDBContext>().Database.Migrate();
        }

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var sqlConnectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<SQLServerDBContext>(o => o.UseSqlServer(sqlConnectionString));

            // Add repositories
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IShowRepository, ShowRepository>();
        }
    }
}
