

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

            var artistIds = songModel.ArtistIds.Select(a => a.ToString()).ToArray();

            var artist = await _repo.GetByIdsAsync<Artist>(artistIds);


            var mappedSong = new Song()
            {
                Title = songModel.Title,
                Duration = songModel.Duration,
                ReleaseDate = songModel.ReleaseDate,
                AlbumId = songModel.AlbumId,
                GenreId = songModel.GenreId
            };

            await _repo.AddAsync(mappedSong);
            _repo.SaveChanges();
        }

        public async Task DeleteSong(Guid id)
        {
            var songToDelete = await GetByIdAsync(id);

            if (id == Guid.Empty)
            {
                throw new ArgumentNullException("There is no valid id to create.");
            }

            _repo.Delete(songToDelete);
            _repo.SaveChanges();
        }

        public SongListViewModel GetAllAsync()
        {
            var songs = _repo.All<Song>().ToList();

            if (songs == null)
            {
                throw new ArgumentNullException("There are no genres", nameof(songs));
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

        public async Task<SongViewModel> GetByIdAsync(Guid id)
        {
            var song = await GetByIdAsync(id);

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

        public async Task<Song> GetSongByIdAsync(Guid id)
        {
            var song = await _repo.GetByIdAsync<Song>(id);

            if (song == null)
            {
                throw new ArgumentNullException("There is no such song.");
            }

            return song;
        }

        public async Task<SongViewModel> GetSongWithDetails(Guid id)
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
            //to do: get album, playlist and genre full info

            return
        }

        public Task UpdateSong(SongViewModel songModel)
        {
            throw new NotImplementedException();
        }
    }
}
