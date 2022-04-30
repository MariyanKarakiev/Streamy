using Microsoft.AspNetCore.Http;
using Streamy.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace Streamy.Core.Models
{
    public class SongModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Release date is required")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm\\:ss}", ApplyFormatInEditMode = true)]
        public TimeSpan Duration { get; set; }

        public string? AlbumId { get; set; }
        public AlbumModel? Album { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        public short GenreId { get; set; }
        public GenreModel? Genre { get; set; }

        public IFormFile Image { get; set; }
        public string? ImageUrl { get; set; }



        public IFormFile Song { get; set; }
        public string? SongUrl { get; set; }

        [Required]
        public string[]? ArtistIds { get; set; }


        public string? UserId { get; set; }

        public List<ArtistModel>? Artists { get; set; }
        public List<PlaylistModel>? Playlists { get; set; }
    }
}
