using Microsoft.EntityFrameworkCore;

using Streamy.Core.Contracts;
using Streamy.Core.Models;
using Streamy.Infrastructure.Data.Repositories;
using Streamy.Infrastructure.Models;

namespace Streamy.Core.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IApplicationDbRepository _repo;

        public AlbumService(IApplicationDbRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateAlbum(AlbumModel albumModel)
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
                Title = albumModel.Title,
                Duration = duration,
                ReleaseDate = albumModel.ReleaseDate,
                ArtistId = CheckIdIsGuid(albumModel.ArtistId),
                Songs = songs,
                UserId = albumModel.UserId,
                ImageUrl = albumModel.ImageUrl,
            };

            await _repo.AddAsync(albumToCreate);
            await _repo.SaveChangesAsync();
        }
        public async Task DeleteAlbum(string id)
        {
            var guidId = CheckIdIsGuid(id);

            var album = await _repo
                .All<Album>()
                .Include(s => s.Songs)
                .Where(s => s.Id == guidId)
                .FirstOrDefaultAsync();

            if (album == null)
            {
                throw new ArgumentNullException(nameof(album));
            }
            _repo.Delete(album);
            await _repo.SaveChangesAsync();
        }
        public async Task UpdateAlbum(AlbumModel songModel)
        {
            if (songModel == null)
            {
                throw new ArgumentNullException("No valid model.");
            }

            var album = await _repo.All<Album>()
                .Include(a => a.Songs)
                .FirstOrDefaultAsync(s => s.Id == CheckIdIsGuid(songModel.Id));

            if (album == null)
            {
                throw new ArgumentNullException("No valid album.");
            }

            var songIds = songModel.SongIds.Select(s => CheckIdIsGuid(s));

            var songs = await _repo.All<Song>()
                .Where(a => songIds.Contains(a.Id)).ToListAsync();

            var duration = new TimeSpan(
            songs
            .Sum(s => s.Duration.Ticks));

            album.Title = songModel.Title;
            album.Duration = duration;
            album.ReleaseDate = songModel.ReleaseDate;
            album.ArtistId = CheckIdIsGuid(songModel.ArtistId);
            album.Songs = songs;


            _repo.Update(album);
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
        public async Task<List<AlbumModel>> GetAll(string? userId)
        {
            var albums = await _repo
                .All<Album>()
                .Include(a => a.Artist)
                .Where(a => a.UserId == userId)
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
        public async Task<AlbumModel> GetByIdForUpdateAsync(string? id)
        {
            var guidId = CheckIdIsGuid(id);

            var album = await _repo
                .All<Album>()
                .Include(a => a.Artist)
                .Include(s => s.Songs)
                .Where(s => s.Id == guidId)
                .FirstOrDefaultAsync();

            if (album == null)
            {
                throw new ArgumentNullException("Album not found.");
            }

            var artist = new ArtistModel()
            {
                Id = album.Artist.Id.ToString(),
                Name = album.Artist.Name
            };

            var songs = album.Songs
                .Select(s => new SongModel()
                {
                    Id = s.Id.ToString(),
                    Title = s.Title
                })
                .ToList();

            var mappedAlbum = new AlbumModel()
            {
                Id = album.Id.ToString(),
                Title = album.Title,

                Duration = album.Duration,
                ReleaseDate = album.ReleaseDate,
            };

            return mappedAlbum;

        }
        public async Task<AlbumModel> GetForDetails(string? id)
        {
            var guidId = CheckIdIsGuid(id);

            var album = await _repo
                .All<Album>()
                .Include(s => s.Songs)
                .Where(s => s.Id == guidId)
                .FirstOrDefaultAsync();

            if (album == null)
            {
                throw new ArgumentNullException("Album not found.");
            }

            var mappedAlbum = new AlbumModel()
            {
                Id = album.Id.ToString(),
                Title = album.Title,
                Artist = new ArtistModel()
                {
                    Name = album.Artist.Name
                },
                Duration = album.Duration,
                ReleaseDate = album.ReleaseDate,
                Songs = album.Songs
                .Select(s => new SongModel()
                {
                    Title = s.Title
                })
                .ToList(),
            };

            return mappedAlbum;
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
