using Microsoft.EntityFrameworkCore;
using Streamy.Core.Contracts;
using Streamy.Core.Models;
using Streamy.Infrastructure.Data.Common;
using Streamy.Infrastructure.Data.Repositories;
using Streamy.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamy.Core.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IApplicationDbRepository _repo;

        public AlbumService(IApplicationDbRepository repo)
        {
            _repo = repo;
        }

        //Checked works
        public async Task CreateAlbum(AlbumCreateModel albumModel)
        {
            if (albumModel == null)
            {
                throw new ArgumentNullException("No valid model.");
            }

            if (albumModel.SongIds.Count() == 0)
            {
                throw new ArgumentException("You must add at least one song to the album.");
            }
            Guid[] songIds = albumModel.SongIds.Select(s => CheckIdIsGuid(s)).ToArray();

            var songs = await _repo
                .All<Song>()
                .Where(s => songIds.Contains(s.Id))
                .ToListAsync();

            var duration = new TimeSpan(
              songs
              .Sum(s => s.Duration.Ticks));

            var albumToCreate = new Album()
            {
                Title = albumModel.Album.Title,
                Duration = duration,
                ReleaseDate = albumModel.Album.ReleaseDate,
                ArtistId = CheckIdIsGuid(albumModel.Album.ArtistId),
                Songs = songs
            };

            await _repo.AddAsync(albumToCreate);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAlbum(string id)
        {
            var albumToDelete = await _repo.GetByIdAsync<Album>(id);

            if (albumToDelete == null)
            {
                throw new ArgumentNullException("No valid album found.");
            }

            await _repo.DeleteAsync<Album>(albumToDelete);
            await _repo.SaveChangesAsync();
        }

        public async Task<List<AlbumModel>> GetAll()
        {
            var albums = await _repo
                .All<Album>()
                .Include(a => a.Artist)
                .ToListAsync();

            if (albums == null)
            {
                throw new ArgumentNullException("No albums.");
            }

            var mappedAlbums = albums
                .Select(s => new AlbumModel()
                {
                    Id = s.Id.ToString(),
                    Title = s.Title,
                    ReleaseDate = s.ReleaseDate,
                    Duration = s.Duration,
                    Artist = new ArtistModel()
                    {
                        Name = s.Artist.Name,
                    },
                    Songs = s.Songs
                        .Select(s => new SongModel()
                        {
                            Id = s.Id.ToString(),
                            Title = s.Title,
                        }).ToList()
                })
                .ToList();

            if (albums == null)
            {
                throw new ArgumentNullException("No valid album models.");
            }

            return mappedAlbums;
        }

        public Task<AlbumCreateModel> GetByIdForCreateAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<AlbumModel> GetWithDetails(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAlbum(AlbumCreateModel songModel)
        {
            throw new NotImplementedException();
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
