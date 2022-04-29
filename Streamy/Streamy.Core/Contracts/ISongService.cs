using Streamy.Core.Models;

namespace Streamy.Core.Contracts
{
    public interface ISongService
    {
        Task CreateSong(SongModel songModel);
        Task UpdateSong(SongModel songModel);
        Task DeleteSong(string id);

        Task<ArtistModel> GetByIdForUpdateAsync(string? id);
        Task<List<ArtistModel>> GetAll();
        Task<List<ArtistModel>> GetAll(string? userId);
        Task<ArtistModel> GetForDetails(string? id);
    }
}
