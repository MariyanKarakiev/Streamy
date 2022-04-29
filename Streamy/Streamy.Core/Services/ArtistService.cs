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

        public async Task CreateArtist(ArtistModel artistModel)
        {
            if (artistModel == null)
            {
                throw new ArgumentNullException("There is no artist to create.");
            }

            await _repo.AddAsync(new Artist
            {
                Name = artistModel.Name,
                Country = artistModel.Country,
                UserId = artistModel.UserId
            });

            _repo.SaveChanges();
        }
        public async Task DeleteArtist(string id)
        {
            var artist = await _repo
               .All<Artist>()
               .Include(a => a.Songs)
               .FirstOrDefaultAsync(a => a.Id == CheckIdIsGuid(id));

            _repo.Delete<Artist>(artist);
            await _repo.SaveChangesAsync();
        }
        public async Task UpdateArtist(ArtistModel artistModel)
        {
            if (artistModel == null)
            {
                throw new ArgumentNullException("Invalid model.", nameof(artistModel));
            }

            var artist = await _repo.All<Artist>().FirstOrDefaultAsync(a => a.Id == CheckIdIsGuid(artistModel.Id));

            if (artist == null)
            {
                throw new ArgumentNullException("Artist not found", nameof(artist));
            }

            artist.Name = artistModel.Name;
            artist.Country = artistModel.Country;

            _repo.Update(artist);
            _repo.SaveChanges();
        }


        public async Task<List<ArtistModel>> GetAll()
        {
            var artists = await _repo.All<Artist>()
                .ToListAsync();

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
        public async Task<List<ArtistModel>> GetAll(string? userId)
        {
            var artists = await _repo.All<Artist>()
                .Where(a => a.UserId == userId)
                .ToListAsync();

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
        public async Task<ArtistModel> GetForDetails(string? id)
        {
            var artist = await _repo.GetByIdAsync<Artist>(id);

            var mappedArtist = new ArtistModel()
            {
                Id = artist.Id.ToString(),
                Name = artist.Name,
                Country = artist.Country
            };

            return mappedArtist;
        }
        public async Task<ArtistModel> GetByIdForUpdateAsync(string? id)
        {
            var artist = await _repo.GetByIdAsync<Artist>(id);

            if (artist == null)
            {
                throw new ArgumentNullException("no artist.");
            }

            var mappedArtist = new ArtistModel()
            {
                Id = artist.Id.ToString(),
                Name = artist.Name,
                Country = artist.Country
            };

            return mappedArtist;
        }

        private Guid CheckIdIsGuid(string id)
        {
            if (!Guid.TryParse(id, out var guidId))
            {
                throw new InvalidCastException("Invalid id.");
            }

            return guidId;
        }

     
    }
}
