using Microsoft.EntityFrameworkCore;
using Streamy.Core.Contracts;
using Streamy.Core.Models;
using Streamy.Infrastructure.Data.Repositories;
using Streamy.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamy.Core.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IApplicationDbRepository _repo;
       
        public ArtistService(IApplicationDbRepository repo)
        {
                _repo = repo;
        }

        public Task CreateArtist(ArtistModel albumModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteArtist(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ArtistModel>> GetAll()
        {
            var artists = await _repo.All<Artist>().ToListAsync();

            if (artists == null)
            {
                throw new AbandonedMutexException(nameof(artists));
            }


            var mappedArtists = artists
                .Select(s => new ArtistModel()
                {
                  Id = s.Id.ToString(),
                  Name = s.Name,
                  Country = s.Country,
                })
                .ToList();

            if (mappedArtists == null)
            {
                throw new ArgumentNullException("No valid artist models.");
            }

            return mappedArtists;
        }
    

        public Task<ArtistModel> GetByIdForCreateAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ArtistModel> GetWithDetails(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateArtist(ArtistModel albumModel)
        {
            throw new NotImplementedException();
        }
    }
}
