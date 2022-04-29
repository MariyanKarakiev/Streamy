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
    public class PlaylistService : IPlaylistService
    {
        private readonly IApplicationDbRepository _repo;

        public PlaylistService(IApplicationDbRepository repo)
        {
            _repo = repo;
        }

        public async Task CreatePlaylist(PlaylistModel playlistModel)
        {
            if (playlistModel == null)
            {
                throw new ArgumentNullException("No valid model.");
            }

            if (playlistModel.SongIds.Count() == 0)
            {
                throw new ArgumentException("You must add at least one song to the playlist.");
            }

            Guid[] songIds = playlistModel.SongIds.Select(s => CheckIdIsGuid(s)).ToArray();

            var songs = await _repo
                .All<Song>()
                .Where(s => songIds.Contains(s.Id))
                .ToListAsync();

            if (songs == null)
            {
                throw new ArgumentNullException("No valid songs to add.");
            }

            var playlistToCreate = new Playlist()
            {
                Title = playlistModel.Title,
                Songs = songs,
                UserId = playlistModel.UserId,
            };

            await _repo.AddAsync(playlistToCreate);
            await _repo.SaveChangesAsync();
        }
        public async Task DeletePlaylist(string id)
        {
            var guidId = CheckIdIsGuid(id);

            var playlist = await _repo
                .All<Playlist>()
                .Include(s => s.Songs)
                .Where(s => s.Id == guidId)
                .FirstOrDefaultAsync();

            if (playlist == null)
            {
                throw new ArgumentNullException("No valid playlist to delete.");
            }
            _repo.Delete(playlist);
            await _repo.SaveChangesAsync();
        }
        public async Task UpdatePlaylist(PlaylistModel playlistModel)
        {
            if (playlistModel == null)
            {
                throw new ArgumentNullException("No valid model.");
            }

            var playlist = await _repo.All<Playlist>()
                .Include(a => a.Songs)
                .FirstOrDefaultAsync(s => s.Id == CheckIdIsGuid(playlistModel.Id));

            if (playlist == null)
            {
                throw new ArgumentNullException("No valid album.");
            }

            var songIds = playlistModel.SongIds.Select(s => CheckIdIsGuid(s));

            var songs = await _repo.All<Song>()
                .Where(a => songIds.Contains(a.Id)).ToListAsync();


            playlist.Title = playlistModel.Title;
            playlist.Songs = songs;


            _repo.Update(playlist);
            await _repo.SaveChangesAsync();
        }



        public async Task<List<PlaylistModel>> GetAll()
        {
            var playlists = await _repo.All<Playlist>()
                 .Include(a => a.Songs)
                 .ToListAsync();

            if (playlists == null)
            {
                throw new ArgumentNullException("No playlists.");
            }

            var mappedPlaylists = playlists
                .Select(s => new PlaylistModel()
                {
                    Id = s.Id.ToString(),
                    Title = s.Title,
                    Songs = s.Songs
                        .Select(s => new SongModel()
                        {
                            Title = s.Title,
                        }).ToList()
                })
                .ToList();

            if (mappedPlaylists == null)
            {
                throw new ArgumentNullException("No valid playlists models.");
            }

            return mappedPlaylists;
            public async Task<List<PlaylistModel>> GetAll()
            {
                var playlists = await _repo.All<Playlist>()
                     .Include(a => a.Songs)
                     .ToListAsync();

                if (playlists == null)
                {
                    throw new ArgumentNullException("No playlists.");
                }

                var mappedPlaylists = playlists
                    .Select(s => new PlaylistModel()
                    {
                        Id = s.Id.ToString(),
                        Title = s.Title,
                        Songs = s.Songs
                            .Select(s => new SongModel()
                            {
                                Title = s.Title,
                            }).ToList()
                    })
                    .ToList();

                if (mappedPlaylists == null)
                {
                    throw new ArgumentNullException("No valid playlists models.");
                }

                return mappedPlaylists;
            }
        }
        public async Task<List<PlaylistModel>> GetAll(string? userId)
        {
            var playlists = await _repo.All<Playlist>()
                 .Include(a => a.Songs)
                 .Where(a => a.UserId == userId)
                 .ToListAsync();

            if (playlists == null)
            {
                throw new ArgumentNullException("No playlists.");
            }

            var mappedPlaylists = playlists
                .Select(s => new PlaylistModel()
                {
                    Id = s.Id.ToString(),
                    Title = s.Title,
                    Songs = s.Songs
                        .Select(s => new SongModel()
                        {
                            Title = s.Title,
                        }).ToList()
                })
                .ToList();

            if (mappedPlaylists == null)
            {
                throw new ArgumentNullException("No valid playlists models.");
            }

            return mappedPlaylists;
        }
        public async Task<PlaylistModel> GetByIdForUpdateAsync(string? id)
        {
            var guidId = CheckIdIsGuid(id);

            var playlist = await _repo
                .All<Playlist>()
                .Include(s => s.Songs)
                .Where(s => s.Id == guidId)
                .FirstOrDefaultAsync();

            if (playlist == null)
            {
                throw new ArgumentNullException("Playlist not found.");
            }


            var songs = playlist.Songs
                .Select(s => new SongModel()
                {
                    Id = s.Id.ToString(),
                    Title = s.Title
                })
                .ToList();

            var mappedPlaylist = new PlaylistModel()
            {
                Id = playlist.Id.ToString(),
                Title = playlist.Title,
                Songs = songs
            };

            return mappedPlaylist;
        }
        public Task<PlaylistModel> GetForDetails(string? id)
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
