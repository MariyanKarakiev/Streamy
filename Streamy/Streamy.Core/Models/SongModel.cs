using Microsoft.AspNetCore.Http;
using Streamy.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace Streamy.Core.Models
{
    public class SongModel
    {
        public string? Id { get; set; }

        [Required]
        public string Title { get; set; }


        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm\\:ss}", ApplyFormatInEditMode = true)]
        public TimeSpan Duration { get; set; }

        public string? AlbumId { get; set; }
        public AlbumModel? Album { get; set; }

        [Required]
        public short GenreId { get; set; }
        public GenreModel? Genre { get; set; }

        [Required]
        public IFormFile Image { get; set; }
        public string? ImageUrl { get; set; }

        public string? UserId { get; set; }

        [Required]
        public string[]? ArtistIds { get; set; }


        public List<ArtistModel>? Artists { get; set; }
        public List<PlaylistModel>? Playlists { get; set; }
    }
}
