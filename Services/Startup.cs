using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Services;
using AutoMapper;
using Services.Mapping;

namespace Services
{
    public class Startup
    {
        public static void Configure(IServiceProvider serviceProvider)
        {
            Domain.Startup.Configure(serviceProvider);
        }

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            Domain.Startup.ConfigureServices(services, configuration);

            services.AddTransient<IShowService, ShowService>();

            services.AddAutoMapper(typeof(PersonDtoMapper), typeof(ShowDtoMapper));
        }
    }
}
