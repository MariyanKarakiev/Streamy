using Streamy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamy.Core.Contracts
{
    public interface ISongService
    {
        Task CreateSong(SongViewModel songModel);
        Task UpdateSong(SongViewModel songModel);
        void DeleteSong(SongViewModel songModel);
        
        Task<SongViewModel> GetByIdAsync(Guid id);
        Task<List<SongViewModel>> GetAllAsync();
        Task<SongViewModel> GetGenreWithDetails(Guid id);
    }
}
