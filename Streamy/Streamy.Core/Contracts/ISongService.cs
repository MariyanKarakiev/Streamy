using Streamy.Core.Models;

namespace Streamy.Core.Contracts
{
    public interface ISongService
    {
        Task CreateSong(SongModel songModel);
        Task UpdateSong(SongModel songModel);
        Task DeleteSong(string id);

        Task<SongModel> GetByIdForUpdateAsync(string? id);
        Task<List<SongModel>> GetAll();
        Task<List<SongModel>> GetAll(string? userId);
        Task<SongModel> GetForDetails(string? id);
    }
}
