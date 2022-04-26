using Streamy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamy.Core.Contracts
{
    public interface IArtistService
    {
        Task CreateArtist(ArtistModel albumModel);
        Task UpdateArtist(ArtistModel albumModel);
        Task DeleteArtist(string id);


        Task<ArtistModel> GetByIdForCreateAsync(string id);
        Task<List<ArtistModel>> GetAll();
        Task<ArtistModel> GetWithDetails(string id);
    }
}
