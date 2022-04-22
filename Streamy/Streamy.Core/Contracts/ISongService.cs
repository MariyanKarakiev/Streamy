using Streamy.Core.Models;

namespace Streamy.Core.Contracts
{
    public interface ISongService
    {
        Task CreateSong(SongViewModel songModel);
        Task UpdateSong(SongViewModel songModel);
        Task DeleteSong(Guid id);

        Task<SongViewModel> GetByIdAsync(Guid id);
        SongListViewModel GetAllAsync();
        Task<SongViewModel> GetSongWithDetails(Guid id);
    }
}
