using Streamy.Core.Models;

namespace Streamy.Core.Contracts
{
    public interface ISongService
    {
        Task CreateSong(SongViewModel songModel);
        Task UpdateSong(SongViewModel songModel);
        Task DeleteSong(string id);

        Task<SongViewModel> GetByIdAsync(string id);
        SongListViewModel GetAll();
        Task<SongViewModel> GetSongWithDetails(string id);
    }
}
