using Moq;
using NUnit.Framework;
using Streamy.Core.Services;
using Streamy.Infrastructure.Data.Repositories;
using Streamy.Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Streamy_Tests
{
    public class GenreServiceTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task ReturnsCorrectGenreById()
        {
            var repo = new Mock<IApplicationDbRepository>();
                repo.Setup( r => r.All<Genre>()).Returns(TestGenres().AsQueryable());

            var service = new GenreService(repo.Object);
            var genre = await service.GetByIdForUpdateAsync(1);

            Assert.That(genre.Id, Is.EqualTo(1));
            Assert.That(genre.Name, Is.EqualTo("Rock"));
        }

        public List<Genre> TestGenres()
        {
            return new List<Genre>()
            {
                new Genre()
                {
                    Id = 1,
                    Name = "Rock"
                },
                new Genre()
                {
                Id = 2,
                Name = "Pop"
                }
            };
        }
    }
}