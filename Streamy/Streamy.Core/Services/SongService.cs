

using Microsoft.EntityFrameworkCore;
using Streamy.Core.Contracts;
using Streamy.Core.Models;

using Streamy.Infrastructure.Data.Repositories;
using Streamy.Infrastructure.Models;

namespace Streamy.Core.Services
{
    public class SongService : ISongService
    {
        private readonly IApplicationDbRepository _repo;

        public SongService(IApplicationDbRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateSong(SongModel songModel)
        {
            if (songModel == null)
            {
                throw new ArgumentNullException("There is no song to create.");
            }

            Guid[] artistIds = songModel.ArtistIds.Select(a => CheckIdIsGuid(a)).ToArray();

            var artists = new List<Artist>();
            var playlists = new List<Playlist>();

            if (artistIds != null)
            {
                foreach (Guid artistId in artistIds)
                {
                    var artist = await _repo.GetByIdAsync<Artist>((object)artistId);

                    if (artist == null)
                    {
                        throw new ArgumentNullException("There is no valid artist.");
                    }

                    artists.Add(artist);
                }
            }

            var mappedSong = new Song()
            {
                Title = songModel.Title,
                Duration = songModel.Duration,
                GenreId = songModel.GenreId,
                Artists = artists,
                Playlists = playlists,
                UserId = songModel.UserId,
            };

            if (songModel.AlbumId != null)
            {
                mappedSong.AlbumId = CheckIdIsGuid(songModel.AlbumId);
            }

            await _repo.AddAsync(mappedSong);
            _repo.SaveChanges();
        }
        public async Task DeleteSong(string id)
        {
            var song = await _repo
                .All<Song>()
                .FirstOrDefaultAsync(s => s.Id == CheckIdIsGuid(id));

            if (song == null)
            {
                throw new Exception();
            }

            _repo.Delete(song);
            _repo.SaveChanges();
        }

        public async Task UpdateSong(SongModel songModel)
        {
            List<Guid> artistIds = new List<Guid>();
            List<Guid> playlistIds = new List<Guid>();

            if (songModel == null)
            {
                throw new ArgumentNullException("No valid model.");
            }


            if (songModel.ArtistIds != null)
            {
                artistIds = songModel
                         .ArtistIds
                         .Select(ai => CheckIdIsGuid(ai))
                         .ToList();
            }

            var songId = CheckIdIsGuid(songModel.Id);

            var artists = await _repo.All<Artist>()
                .Where(a => artistIds.Contains(a.Id)).ToListAsync();

            var song = await _repo.All<Song>()
                .Include(a => a.Genre)
                .Include(a => a.Album)
                .Include(a => a.Artists)
                .FirstOrDefaultAsync(s => s.Id == songId);

            if (song == null)
            {
                throw new ArgumentNullException("No valid song.");
            }


            song.Title = songModel.Title;
            song.Duration = songModel.Duration;
            song.ReleaseDate = songModel.ReleaseDate;
            song.GenreId = songModel.GenreId;
            song.Artists = artists;

            if (songModel.AlbumId != null)
            {

                song.AlbumId = CheckIdIsGuid(songModel.AlbumId);

            }

            _repo.Update(song);
            _repo.SaveChanges();
        }

        //Ready
        public async Task<List<SongModel>> GetAll()
        {
            var songs = await _repo
                .All<Song>()
                .Include(s => s.Artists)
                .ToListAsync();

            if (songs == null)
            {
                throw new ArgumentNullException("There are no songs", nameof(songs));
            }

            var songModelList = new List<SongModel>();

            foreach (var s in songs)
            {
                songModelList
                    .Add(new SongModel
                    {
                        Id = s.Id.ToString(),
                        Title = s.Title,
                        Duration = s.Duration,
                        ReleaseDate = s.ReleaseDate,
                        GenreId = s.GenreId,
                        Artists = s.Artists
                        .Select(x => new ArtistModel()
                        {
                            Id = x.Id.ToString(),
                            Name = x.Name
                        })
                        .ToList()
                    });
            }

            return songModelList;
        }
        public async Task<List<SongModel>> GetAll(string? userId)
        {
            var songs = await _repo
                .All<Song>()
                .Include(s => s.Artists)
                .Where(s => s.UserId == userId)
                .ToListAsync();

            if (songs == null)
            {
                throw new ArgumentNullException("There are no songs", nameof(songs));
            }

            var songModelList = new List<SongModel>();

            foreach (var s in songs)
            {
                songModelList
                    .Add(new SongModel
                    {
                        Id = s.Id.ToString(),
                        Title = s.Title,
                        Duration = s.Duration,
                        ReleaseDate = s.ReleaseDate,
                        GenreId = s.GenreId,
                        UserId = userId,
                        Artists = s.Artists
                        .Select(x => new ArtistModel()
                        {
                            Id = x.Id.ToString(),
                            Name = x.Name
                        })
                        .ToList()
                    });
            }

            return songModelList;
        }
        public async Task<SongModel> GetByIdForUpdateAsync(string? id)
        {
            var song = await GetSongWithDetails(id);

            var mappedSongCreateModel = new SongModel()
            {
                Id = song.Id.ToString(),
                AlbumId = song.AlbumId,
                Duration = song.Duration,
                GenreId = song.Genre.Id,
                Playlists = song.Playlists,
                Artists = song.Artists,
                Title = song.Title,
                ReleaseDate = song.ReleaseDate,
            };

            return mappedSongCreateModel;
        }
        public async Task<SongModel> GetForDetails(string? id)
        {
            var guidId = CheckIdIsGuid(id);

            var song = await _repo
                .All<Song>()
                .Include(s => s.Album)
                .Include(s => s.Artists)
                .Include(s => s.Playlists)
                .Include(s => s.Genre)
                .Where(s => s.Id == guidId)
                .FirstOrDefaultAsync();

            if (song == null)
            {
                throw new ArgumentNullException("No song to display.");
            }

            var mappedGenre = new GenreModel()
            {
                Id = song.Genre.Id,
                Name = song.Genre.Name,
            };

            var mappedArtists = song
                .Artists
                .Select(a => new ArtistModel()
                {
                    Id = a.Id.ToString(),
                    Name = a.Name,
                })
                .ToList();

            var mappedAlbum = new AlbumModel();

            if (song.Album != null)
            {
                mappedAlbum = new AlbumModel()
                {
                    Id = song.Album.Id.ToString(),
                    Title = song.Album.Title,
                };
            }
            var mappedPlaylists = song
                .Playlists
                .Select(a => new PlaylistModel()
                {
                    Id = a.Id.ToString(),
                    Title = a.Title,
                })
                .ToList();


            var mappedSong = new SongModel()
            {
                Id = song.Id.ToString(),
                Title = song.Title,
                Duration = song.Duration,
                ReleaseDate = song.ReleaseDate,
                Album = mappedAlbum,
                Genre = mappedGenre,
                Artists = mappedArtists,
                Playlists = mappedPlaylists
            };

            return mappedSong;
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
