﻿

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

            var genre = await _repo.GetByIdAsync<Genre>(songModel.GenreId);

            foreach (Guid artistId in artistIds)
            {
                var artist = await _repo.GetByIdAsync<Artist>((object)artistId);

                if (artist == null)
                {
                    throw new ArgumentNullException("There is no valid artist.");
                }

                artists.Add(artist);
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
                ReleaseDate = songModel.ReleaseDate,
                //  AlbumId = CheckIdIsGuid(songModel.AlbumId),
                Genre = genre,
                Artists = artists,
                Playlists = playlists
            };


            await _repo.AddAsync(mappedSong);
            _repo.SaveChanges();
        }
        public async Task DeleteSong(string id)
        {
            var songToDelete = await GetSongByIdAsync(id);


            _repo.Delete(songToDelete);
            _repo.SaveChanges();
        }
        public Task UpdateSong(SongCreateModel songModel)
        {
            if (songModel == null)
            {
                throw new ArgumentNullException("No valid model.");
            }

            var song = new Song()
            {
                Id = CheckIdIsGuid(songModel.Id),
                Title = songModel.Title,
                Duration = songModel.Duration,
                ReleaseDate = songModel.ReleaseDate,
                //  AlbumId = songModel.AlbumId,
                GenreId = songModel.GenreId,

                Artists = songModel
                .Artists
                .Select(a => new Artist()
                {
                    Id = CheckIdIsGuid(a.Id),
                    Name = a.Name,
                    Country = a.Country,
                })
                .ToList(),

                Playlists = songModel
                .Playlists
                .Select(p => new Playlist()
                {
                    Id = CheckIdIsGuid(p.Id),
                    Title = p.Title
                })
                .ToList()
            };

            _repo.Update(song);
            _repo.SaveChanges();

            return Task.CompletedTask;
        }

        public SongListModel GetAll()
        {
            var songs = _repo.All<Song>().ToList();

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
                      //  AlbumId = s.AlbumId,
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
                Album = song.Album,
                Genre = song.Genre,
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
