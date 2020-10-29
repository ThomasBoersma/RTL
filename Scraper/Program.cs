using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Scraper.Models;
using Domain;
using Domain.Repositories;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Binder;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Scraper.Mapping;


namespace Scraper
{
    class Program
    {
        static void Main()
        {
            var serviceProvider = ConfigureServices().BuildServiceProvider();

            var scraper = serviceProvider.GetService<Scraper>();

            scraper.Start();
        }

        private static IServiceCollection ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            var configuration = LoadConfiguration();
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddTransient<Scraper>();
            serviceCollection.AddAutoMapper(typeof(PersonMapper), typeof(ShowMapper));

            Startup.ConfigureServices(serviceCollection, configuration);

            return serviceCollection;
        }

        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }

    }
}
