using Streamy.Core.Models;
using Streamy.Core.Models.Song;

namespace Streamy.Core.Contracts
{
    public interface ISongService
    {
        Task CreateSong(SongCreateModel songModel);
        Task UpdateSong(SongCreateModel songModel);
        Task DeleteSong(string id);

        Task<SongModel> GetByIdAsync(string id);
        Task<SongCreateModel> GetByIdForCreateAsync(string id);
        SongListModel GetAll();
        Task<SongModel> GetSongWithDetails(string id);
    }
}
