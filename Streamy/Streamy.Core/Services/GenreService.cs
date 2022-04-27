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

        //Checked, ready, works
        public async Task CreateGenre(GenreModel genreModel)
        {
            if (genreModel == null)
            {
                throw new ArgumentNullException("There is no genre to create.");
            }

            await _repo.AddAsync(new Genre
            {
                Name = genreModel.Name,
            });

            _repo.SaveChanges();
        }
       
        //Checked, ready, works
        public async Task DeleteGenre(short id)
        {
            await _repo.DeleteAsync<Genre>(id);
            await _repo.SaveChangesAsync();
        }

        //Ok
        public async Task<List<GenreModel>> GetAllGenres()
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

        //Ok
        public async Task<GenreModel> GetByIdAsync(short id)
        {
            var genre = await _repo.GetByIdAsync<Genre>(id);
           
            var mappedGenre = new GenreModel()
            {
                Id = genre.Id,
                Name = genre.Name
            };

            return mappedGenre;
        }

        //Checked, ready, works
        public async Task<GenreModel> GetGenreWithDetails(short id)
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

        //Checked, ready, works
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

            genre.Id = genreModel.Id;
            genre.Name = genreModel.Name;

            _repo.Update(genre);
            _repo.SaveChanges();
        }
    }
}
