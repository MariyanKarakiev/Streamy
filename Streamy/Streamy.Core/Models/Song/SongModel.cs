using Streamy.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace Streamy.Core.Models
{
    public class SongModel
    {
        public string Id { get; set; }

       
        public string Title { get; set; }


        public DateTime ReleaseDate { get; set; }


        public TimeSpan Duration { get; set; }

        public string? AlbumId { get; set; }
        public AlbumViewModel Album { get; set; }

        public short GenreId { get; set; }
        public GenreViewModel Genre { get; set; }


        public List<ArtistViewModel> Artists { get; set; } = new List<ArtistViewModel>();
        public List<PlaylistViewModel> Playlists { get; set; } = new List<PlaylistViewModel>();
    }
}
