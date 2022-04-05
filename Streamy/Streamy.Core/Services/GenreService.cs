using Streamy.Core.Models;
using Streamy.Infrastructure.Data.Repositories;
using Streamy.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamy.Core.Services
{
    public class GenreService : IGenreService
    {
        private readonly IApplicationDbRepository _repo;

        public GenreService(IApplicationDbRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateGenre(GenreModel genreModel)
        {
           await _repo.AddAsync(new Genre
            {
                Name = genreModel.Name,
            });

            _repo.SaveChanges();
        }

        public Task DeleteGenre(string genreId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateGenre(GenreModel genreModel)
        {
            throw new NotImplementedException();
        }
    }
}
