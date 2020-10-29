using System;
using Domain.Repositories;
using AutoMapper;
using Scraper.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Diagnostics;


namespace Scraper
{
    public class Scraper
    {
        private HttpClient client = new HttpClient();
        private Stopwatch stopWatch = new Stopwatch();

        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IShowRepository showRepository;
        private readonly IPersonRepository personRepository;

        public Scraper(IMapper mapper, IConfiguration configuration, IShowRepository showRepository, IPersonRepository personRepository)
        {
            this.mapper = mapper;
            this.configuration = configuration;
            this.showRepository = showRepository;
            this.personRepository = personRepository;
        }

        public void Start()
        {
            RunAsync().GetAwaiter().GetResult();
            Console.ReadLine();
        }

        private async Task RunAsync()
        {
            Console.WriteLine("Start scraping");
            Console.WriteLine("Working ...");

            client.BaseAddress = new Uri("http://api.tvmaze.com/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Start the timer for rate limits
                stopWatch.Start();

                var shows = await GetShowsAsync(configuration.GetSection("TvMaze").GetValue<int>("StartAtPage"));

                // Analytics
                int amountOfPersons = 0;
                int amountOfRequests = 1;

                for (int i = 0; i < shows.Count; i++)
                {
                    Show show = shows[i];

                    var roles = await GetShowRolesAsync(show.Id);
                    show.Cast = roles.Select(x => x.Person).ToList();

                    await showRepository.AddAsync(mapper.Map<Domain.Models.Show>(show));

                    // Analytics
                    amountOfRequests++;
                    amountOfPersons += show.Cast.Count;

                    CheckRateLimit(i);
                }

                Console.WriteLine($"Done scraping! Scraped {shows.Count} shows and {amountOfPersons} persons in {amountOfRequests} requests.");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error: {exception.Message}");
            }
        }

        /// <summary>
        /// Check if the rate limit is exceeded and sleep if so
        /// </summary>
        /// <param name="i">Call number</param>
        private void CheckRateLimit(int i)
        {
            if (i % configuration.GetSection("TvMaze").GetValue<int>("CallLimit") == 0)
            {
                stopWatch.Stop();
                int elapsed = configuration.GetSection("TvMaze").GetValue<int>("CallRefreshTimeInSeconds") - stopWatch.Elapsed.Seconds;

                if (elapsed > 0)
                {
                    Thread.Sleep(elapsed * 1000);
                }

                stopWatch.Restart();
            }
        }

        /// <summary>
        /// Get the shows
        /// </summary>
        /// <param name="page"></param>
        /// <returns>A list of shows</returns>
        private async Task<List<Show>> GetShowsAsync(int page)
        {
            List<Show> shows = null;
            HttpResponseMessage response = await client.GetAsync($"shows?page={page}");
            if (response.IsSuccessStatusCode)
            {
                shows = await response.Content.ReadAsAsync<List<Show>>();
            }
            return shows;
        }

        /// <summary>
        /// Get the cast of a show
        /// </summary>
        /// <param name="showId"></param>
        /// <returns>A list of roles containing a person object</returns>
        private async Task<List<Role>> GetShowRolesAsync(int showId)
        {
            List<Role> roles = null;
            HttpResponseMessage response = await client.GetAsync($"shows/{showId}/cast");
            if (response.IsSuccessStatusCode)
            {
                roles = await response.Content.ReadAsAsync<List<Role>>();
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }

            return roles;
        }

    }
}
