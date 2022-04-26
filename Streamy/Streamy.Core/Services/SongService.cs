

using Microsoft.EntityFrameworkCore;
using Streamy.Core.Contracts;
using Streamy.Core.Models;
using Streamy.Core.Models.Song;
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

        public async Task CreateSong(SongCreateModel songModel)
        {
            if (songModel == null)
            {
                throw new ArgumentNullException("There is no song to create.");
            }

            Guid[] artistIds = songModel.Artists.Select(a => Guid.Parse(a.Id)).ToArray();
            Guid[] playlistIds = songModel.Playlists.Select(p => Guid.Parse(p.Id)).ToArray();

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
            if (playlistIds != null)
            {
                foreach (Guid playlistId in playlistIds)
                {
                    var playlist = await _repo.GetByIdAsync<Playlist>((object)playlistId);

                    if (playlist == null)
                    {
                        throw new ArgumentNullException("There is no valid playlist.");
                    }

                    playlists.Add(playlist);
                }
            }

            var mappedSong = new Song()
            {
                Title = songModel.Title,
                Duration = songModel.Duration,
                GenreId = songModel.GenreId,
                Artists = artists,
                Playlists = playlists
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
            var songToDelete = await GetSongByIdAsync(id);


            _repo.Delete(songToDelete);
            _repo.SaveChanges();
        }
        public async Task UpdateSong(SongCreateModel songModel)
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

            if (songModel.PlaylistIds != null)
            {
                playlistIds = songModel
                         .PlaylistIds
                         .Select(pi => CheckIdIsGuid(pi))
                         .ToList();
            }

            var songId = CheckIdIsGuid(songModel.Id);

            var artists = await _repo.All<Artist>()
                .Where(a => artistIds.Contains(a.Id)).ToListAsync();

            var playlist = _repo.All<Playlist>()
                .Where(a => playlistIds.Contains(a.Id)).ToListAsync();

            var song = await _repo.All<Song>()
                .Include(a => a.Genre)
                .Include(a => a.Album)
                .Include(a => a.Artists)
                .Include(a => a.Playlists)
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
            // song.Playlists = playlist;

            if (songModel.AlbumId != null)
            {

                song.AlbumId = CheckIdIsGuid(songModel.AlbumId);

            }

            _repo.Update(song);
            _repo.SaveChanges();
        }

        public async Task<SongListModel> GetAll()
        {
            var songs =await _repo.All<Song>().ToListAsync();

            if (songs == null)
            {
                throw new ArgumentNullException("There are no songs", nameof(songs));
            }

            var songListModel = new SongListModel();

            foreach (var s in songs)
            {
                songListModel
                    .Songs
                    .Add(new SongModel
                    {
                        Id = s.Id.ToString(),
                        Title = s.Title,
                        Duration = s.Duration,
                        ReleaseDate = s.ReleaseDate,
                        GenreId = s.GenreId
                    });
            }

            return songListModel;
        }
        public async Task<SongModel> GetByIdAsync(string id)
        {
            var song = await GetSongByIdAsync(id);

            var mappedSong = new SongModel()
            {
                Id = song.Id.ToString(),
                Title = song.Title,
                Duration = song.Duration,
                ReleaseDate = song.ReleaseDate,
                //  AlbumId = song.AlbumId,
                GenreId = song.GenreId
            };

            return mappedSong;
        }

        public async Task<SongCreateModel> GetByIdForCreateAsync(string id)
        {
            var song = await GetSongWithDetails(id);

            var allGenres = await _repo.All<Genre>().ToListAsync();
            var allAlbums = await _repo.All<Album>().ToListAsync();
            var allArtists = await _repo.All<Artist>().ToListAsync();


            //a lot of mapping...needs to be improved
            var mappedSong = new SongCreateModel()
            {
                Id = song.Id.ToString(),
                AlbumId = song.AlbumId,
                Duration = song.Duration,
                GenreId = song.Genre.Id,
                Playlists = song.Playlists,
                Artists = song.Artists,
                Title = song.Title,
                ReleaseDate = song.ReleaseDate,

                Genres = allGenres
                .Select(a => new GenreViewModel()
                {
                    Id = a.Id,
                    Name = a.Name
                })
                .ToList(),

                Albums = allAlbums
                    .Select(a => new AlbumViewModel()
                    {
                        Id = a.Id.ToString(),
                        Title = a.Title,
                        Duration = a.Duration,
                        ReleaseDate = a.ReleaseDate
                    })
                .ToList(),

                AllArtists = allArtists
                 .Select(a => new ArtistViewModel()
                 {
                     Id = a.Id.ToString(),
                     Name = a.Name,
                     Country = a.Country,
                 })
                .ToList(),
            };

            return mappedSong;
        }
        private async Task<Song> GetSongByIdAsync(string id)
        {
            var guidId = CheckIdIsGuid(id);

            var song = await _repo.GetByIdAsync<Song>((object)guidId);

            if (song == null)
            {
                throw new ArgumentNullException("There is no such song.");
            }

            return song;
        }
        public async Task<SongModel> GetSongWithDetails(string id)
        {
            var guidId = CheckIdIsGuid(id);

            var song = await _repo
                .All<Song>()
                .Include(s => s.Artists)
                .Include(s => s.Playlists)
                .Include(s => s.Genre)
                .Where(s => s.Id == guidId)
                .FirstOrDefaultAsync();

            if (song == null)
            {
                throw new ArgumentNullException("No song to display.");
            }

            var mappedGenre = new GenreViewModel()
            {
                Id = song.Genre.Id,
                Name = song.Genre.Name,
            };

            var mappedArtists = song
                .Artists
                .Select(a => new ArtistViewModel()
                {
                    Id = a.Id.ToString(),
                    Name = a.Name,
                })
                .ToList();

            var mappedAlbum = new AlbumViewModel();

            if (song.Album != null)
            {
                mappedAlbum = new AlbumViewModel()
                {
                    Id = song.Album.Id.ToString(),
                    Title = song.Album.Title,
                };
            }
            var mappedPlaylists = song
                .Playlists
                .Select(a => new PlaylistViewModel()
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
