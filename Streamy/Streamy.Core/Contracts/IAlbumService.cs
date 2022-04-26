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
        Task CreateAlbum(AlbumCreateModel albumModel);
        Task UpdateAlbum(AlbumCreateModel albumModel);
        Task DeleteAlbum(string id);


        Task<AlbumCreateModel> GetByIdForCreateAsync(string id);
        Task<List<AlbumModel>> GetAll();
        Task<AlbumModel> GetWithDetails(string id);
    }
}
