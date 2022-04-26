using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamy.Core.Models.Song
{
    public class SongCreateModel
    {
        public string Id { get; set; }


        public string Title { get; set; }


        public DateTime ReleaseDate { get; set; }


        public TimeSpan Duration { get; set; }

        [Display(Name = "Album")]
        public string? AlbumId { get; set; }
        public short GenreId { get; set; }

        public string[] ArtistIds { get; set; }
        public string[] PlaylistIds { get; set; }

        public List<ArtistViewModel> Artists { get; set; }
        public List<PlaylistViewModel> Playlists { get; set; }

        public List<GenreViewModel> Genres { get; set; } = new List<GenreViewModel>();
        public List<AlbumViewModel> Albums { get; set; } = new List<AlbumViewModel>();
        public List<ArtistViewModel> AllArtists { get; set; } = new List<ArtistViewModel>();
        public List<PlaylistViewModel> AllPlaylists { get; set; } = new List<PlaylistViewModel>();
    }
}
