using Xunit;
using Moq;
using Domain.Models;
using Domain.Repositories;
using Services.Services;
using Common.Paging;
using System.Collections.Generic;
using AutoMapper;
using Services.Mapping;
using System.Threading.Tasks;

namespace Tests.Services
{
    public class ShowServiceShould
    {
        [Fact]
        public async Task OrderTheCastBirthdayByDescending()
        {
            var mockShowRepository = new Mock<IShowRepository>();

            PagingOptions pagingOptions = new PagingOptions();

            mockShowRepository.Setup(x => x.GetAsync(pagingOptions)).ReturnsAsync(GetShowsNotOrderd());

            var sut = new ShowService(GetMapper(), mockShowRepository.Object);

            var result = await sut.GetAsync(pagingOptions);

            Assert.Collection(result,
                x => Assert.Equal("Petra", x.Cast[0].Name),
                y => Assert.Equal("Thomas", y.Cast[0].Name)
            );
        }

        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<ShowDtoMapper>();
                opts.AddProfile<PersonDtoMapper>();
            });
            var mapper = config.CreateMapper();

            return mapper;
        }

        private List<Show> GetShowsNotOrderd()
        {
            var persons1 = new List<Person>
            {
                new Person() { Id = 1, Name = "Thomas", Birthday = new System.DateTime(1996, 5, 16) },
                new Person() { Id = 1, Name = "Youri", Birthday = new System.DateTime(1995, 1, 1) },
                new Person() { Id = 1, Name = "Harry", Birthday = new System.DateTime(1993, 5, 11) },
                new Person() { Id = 1, Name = "Petra", Birthday = new System.DateTime(2000, 3, 12) }
            };

            var persons2 = new List<Person>
            {
                new Person() { Id = 1, Name = "Thomas", Birthday = new System.DateTime(2007, 5, 16) },
                new Person() { Id = 1, Name = "Youri", Birthday = new System.DateTime(1995, 1, 1) },
                new Person() { Id = 1, Name = "Harry", Birthday = new System.DateTime(2005, 1, 11) },
                new Person() { Id = 1, Name = "Petra", Birthday = new System.DateTime(2003, 3, 12) }
            };

            List<Show> shows = new List<Show>
            {
                new Show() { Id = 1, Name = "XX", Cast = persons1 },
                new Show() { Id = 2, Name = "YY", Cast = persons2 }
            };

            return shows;
        }
    }
}
