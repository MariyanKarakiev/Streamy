using Streamy.Core.Models;

namespace Streamy.Core.Contracts
{
    public interface IGenreService
    {
        Task CreateGenre(GenreModel genreModel);
        Task UpdateGenre(GenreModel genreModel);
        Task DeleteGenre(short id);

        Task<GenreModel> GetByIdForUpdateAsync(short? id);
        Task<List<GenreModel>> GetAll();
        Task<List<GenreModel>> GetAll(string? userId);
        Task<GenreModel> GetForDetails(short? id);
    }
}
