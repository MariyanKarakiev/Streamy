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

        public List<ArtistModel> Artists { get; set; }
        public List<PlaylistModel> Playlists { get; set; }

        public List<GenreModel> Genres { get; set; } = new List<GenreModel>();
        public List<AlbumModel> Albums { get; set; } = new List<AlbumModel>();
        public List<ArtistModel> AllArtists { get; set; } = new List<ArtistModel>();
        public List<PlaylistModel> AllPlaylists { get; set; } = new List<PlaylistModel>();
    }
}
