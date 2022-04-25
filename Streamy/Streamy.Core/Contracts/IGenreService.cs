using Streamy.Core.Models;

namespace Streamy.Core.Contracts
{
    public interface IGenreService
    {
        List<GenreViewModel> GetAllGenres();
        Task<GenreViewModel> GetByIdAsync(short id);
        Task CreateGenre(GenreViewModel genreModel);
        void UpdateGenre(GenreViewModel genreModel);
        Task DeleteGenre(short id);
        Task<GenreViewModel> GetGenreWithDetails(short id);
    }
}
