using Streamy.Core.Models;

namespace Streamy.Core.Contracts
{
    public interface IGenreService
    {
        Task<List<GenreModel>> GetAllGenres();
        Task<GenreModel> GetByIdAsync(short id);
        Task CreateGenre(GenreModel genreModel);
        Task UpdateGenre(GenreModel genreModel);
        Task DeleteGenre(short id);
        Task<GenreModel> GetGenreWithDetails(short id);
    }
}
