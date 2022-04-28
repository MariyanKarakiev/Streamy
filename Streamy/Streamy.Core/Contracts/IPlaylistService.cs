using Streamy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamy.Core.Contracts
{
    public interface IPlaylistService
    {
        Task CreatePlaylist(PlaylistModel albumModel);
        Task UpdatePlaylist(PlaylistModel albumModel);
        Task DeletePlaylist(string id);


        Task<PlaylistModel> GetByIdForUpdateAsync(string id);
        Task<List<PlaylistModel>> GetAll();
        Task<PlaylistModel> GetForDetail(string id);
    }
}
