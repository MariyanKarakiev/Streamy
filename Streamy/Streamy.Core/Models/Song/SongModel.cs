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
        public AlbumModel Album { get; set; }

        public short GenreId { get; set; }
        public GenreModel Genre { get; set; }


        public List<ArtistModel> Artists { get; set; } = new List<ArtistModel>();
        public List<PlaylistModel> Playlists { get; set; } = new List<PlaylistModel>();
    }
}
