using Microsoft.EntityFrameworkCore;
using Streamy.Core.Contracts;
using Streamy.Core.Models;
using Streamy.Infrastructure.Data.Repositories;
using Streamy.Infrastructure.Models;

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
            if (genreModel == null)
            {
                throw new ArgumentNullException("There is no genre to create.");
            }

            await _repo.AddAsync(new Genre
            {
                Name = genreModel.Name,
                UserId = genreModel.UserId,
            });

            _repo.SaveChanges();
        }
        public async Task DeleteGenre(short id)
        {
            await _repo.DeleteAsync<Genre>(id);
            await _repo.SaveChangesAsync();
        }
        public async Task UpdateGenre(GenreModel genreModel)
        {
            if (genreModel == null)
            {
                throw new ArgumentNullException("Invalid model.", nameof(genreModel));
            }

            var genre = await _repo.All<Genre>().FirstOrDefaultAsync(g => g.Id == genreModel.Id);

            if (genre == null)
            {
                throw new ArgumentNullException("Genre not found", nameof(genre));
            }

            genre.Name = genreModel.Name;

            _repo.Update(genre);
            _repo.SaveChanges();
        }


        public async Task<List<GenreModel>> GetAll()
        {
            var genres = await _repo.All<Genre>().ToListAsync();

            if (genres == null)
            {
                throw new ArgumentNullException("There are no genres", nameof(genres));
            }

            var genreModelList = new List<GenreModel>();

            genres.ForEach(g => genreModelList
            .Add(new GenreModel
            {
                Id = g.Id,
                Name = g.Name
            }));

            return genreModelList;
        }
        public async Task<List<GenreModel>> GetAll(string? userId)
        {
            var genres = await _repo
                .All<Genre>()
                .Where(g => g.UserId == userId)
                .ToListAsync();

            if (genres == null)
            {
                throw new ArgumentNullException("There are no genres", nameof(genres));
            }

            var genreModelList = new List<GenreModel>();

            genres.ForEach(g => genreModelList
            .Add(new GenreModel
            {
                Id = g.Id,
                Name = g.Name
            }));

            return genreModelList;
        }
        public async Task<GenreModel> GetByIdForUpdateAsync(short? id)
        {
            var genre = await _repo.GetByIdAsync<Genre>(id);
           
            var mappedGenre = new GenreModel()
            {
                Id = genre.Id,
                Name = genre.Name
            };

            return mappedGenre;
        }
        public async Task<GenreModel> GetForDetails(short? id)
        {
            var genre = await _repo
                .All<Genre>()
                .Include(g => g.Songs)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (genre == null)
            {
                throw new ArgumentNullException("No genre found.");
            }
            var mappedGenre = new GenreModel()
            {
                Id = genre.Id,
                Name = genre.Name,
                Songs = genre.Songs.Count,
            };

            return mappedGenre;
        }

       

    }
}
