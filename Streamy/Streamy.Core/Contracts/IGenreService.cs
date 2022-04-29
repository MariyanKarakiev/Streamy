using Streamy.Core.Models;

namespace Streamy.Core.Contracts
{
    public interface IGenreService
    {
        Task CreateGenre(GenreModel genreModel);
        Task UpdateGenre(GenreModel genreModel);
        Task DeleteGenre(short id);

        Task<ArtistModel> GetByIdForUpdateAsync(string? id);
        Task<List<ArtistModel>> GetAll();
        Task<List<ArtistModel>> GetAll(string? userId);
        Task<ArtistModel> GetForDetails(string? id);
    }
}
