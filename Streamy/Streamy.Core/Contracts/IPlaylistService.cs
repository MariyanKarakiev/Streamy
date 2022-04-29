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


        Task<ArtistModel> GetByIdForUpdateAsync(string? id);
        Task<List<ArtistModel>> GetAll();
        Task<List<ArtistModel>> GetAll(string? userId);
        Task<ArtistModel> GetForDetails(string? id);
    }
}
