using Streamy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamy.Core.Contracts
{
    public interface IAlbumService
    {
        Task CreateAlbum(AlbumModel albumModel);
        Task UpdateAlbum(AlbumModel albumModel);
        Task DeleteAlbum(string id);


        Task<AlbumModel> GetByIdForUpdateAsync(string? id);
        Task<List<AlbumModel>> GetAll();
        Task<List<AlbumModel>> GetAll(string? userId);
        Task<AlbumModel> GetForDetails(string? id);
    }
}
