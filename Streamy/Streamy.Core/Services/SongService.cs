

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

        public async Task CreateSong(SongViewModel songModel)
        {
            if (songModel == null)
            {
                throw new ArgumentNullException("There is no song to create.");
            }

            Guid[] artistIds = songModel.ArtistList.Select(a => Guid.Parse(a.Id)).ToArray();
            Guid[] playlistIds = songModel.PlaylistList.Select(p => Guid.Parse(p.Id)).ToArray();

            var artists = new List<Artist>();
            var playlists = new List<Playlist>();

            var mappedSong = new Song()
            {
                Title = songModel.Title,
                Duration = songModel.Duration,
                ReleaseDate = songModel.ReleaseDate,
                AlbumId = songModel.AlbumId,
                GenreId = songModel.GenreId
            };

            var songArtistModel = new List<SongArtist>();
            var songPlaylistModel = new List<SongPlaylist>();

            foreach (Guid artistId in artistIds)
            {
                var artist = await _repo.GetByIdAsync<Artist>((object)artistId);

                if (artist == null)
                {
                    throw new ArgumentNullException("There is no valid artist.");
                }

                songArtistModel.Add(new SongArtist()
                {
                    Song = mappedSong,
                    Artist = artist
                });
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

                    songPlaylistModel.Add(new SongPlaylist()
                    {
                        Song = mappedSong,
                        Playlist = playlist
                    });
                }
            }

            await _repo.AddAsync(mappedSong);
            await _repo.AddRangeAsync(songArtistModel);
            await _repo.AddRangeAsync(songPlaylistModel);
            _repo.SaveChanges();
        }

        public async Task DeleteSong(string id)
        {
            var songToDelete = await GetSongByIdAsync(id);

            _repo.Delete(songToDelete);
            _repo.SaveChanges();
        }

        public SongListViewModel GetAll()
        {
            var songs = _repo.All<Song>().ToList();

            if (songs == null)
            {
                throw new ArgumentNullException("There are no songs", nameof(songs));
            }

            var songListModel = new SongListViewModel();

            foreach (var s in songs)
            {
                songListModel
                    .Songs
                    .Add(new SongViewModel
                    {
                        Id = s.Id.ToString(),
                        Title = s.Title,
                        Duration = s.Duration,
                        ReleaseDate = s.ReleaseDate,
                        AlbumId = s.AlbumId,
                        GenreId = s.GenreId
                    });
            }

            return songListModel;
        }
        public async Task<SongViewModel> GetByIdAsync(string id)
        {
            var song = await GetSongByIdAsync(id);

            var mappedSong = new SongViewModel()
            {
                Id = song.Id.ToString(),
                Title = song.Title,
                Duration = song.Duration,
                ReleaseDate = song.ReleaseDate,
                AlbumId = song.AlbumId,
                GenreId = song.GenreId
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
        public async Task<SongViewModel> GetSongWithDetails(string id)
        {
            var guidId = CheckIdIsGuid(id);

            var song = await GetSongByIdAsync(id);

            //to do: get album, playlist and genre full info

            var mappedArtists = song.Artists
                .Select(a => new ArtistViewModel()
                {
                    Id = a.Artist.Id.ToString(),
                    Name = a.Artist.Name,
                    Country = a.Artist.Country,
                })
                .ToList();

            var mappedGenre = new GenreViewModel()
            {
                Id = song.Genre.Id,
                Name = song.Genre.Name,
            };


            var mappedSong = new SongViewModel()
            {
                Id = song.Id.ToString(),
                Title = song.Title,
                Duration = song.Duration,
                ReleaseDate = song.ReleaseDate,
                ArtistList = mappedArtists,
                Genre = mappedGenre
            };

            return mappedSong;
        }

        public Task UpdateSong(SongViewModel songModel)
        {
            throw new NotImplementedException();
        }

        private Guid CheckIdIsGuid(string id)
        {
            if (Guid.TryParse(id, out var guidId))
            {
                throw new InvalidCastException("Invalid id.");
            }

            return guidId;
        }
    }
}
