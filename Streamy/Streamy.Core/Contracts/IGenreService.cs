using Streamy.Core.Models;

namespace Streamy.Core.Contracts
{
    public interface IGenreService
    {
        Task<List<GenreViewModel>> GetAllGenres();
        Task<GenreViewModel> GetByIdAsync(short id);
        Task CreateGenre(GenreViewModel genreModel);
        Task UpdateGenre(GenreViewModel genreModel);
        Task DeleteGenre(short id);
        Task<GenreViewModel> GetGenreWithDetails(short id);
    }
}
