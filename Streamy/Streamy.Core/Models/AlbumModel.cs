using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Streamy.Core.Models
{
    public class AlbumModel
    {
        public string? Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; }


        public DateTime ReleaseDate { get; set; }
        public TimeSpan Duration { get; set; }

        public string ArtistId { get; set; }
        public ArtistModel? Artist { get; set; }

        public List<SongModel>? Songs { get; set; }

        public IFormFile Image { get; set; }
        public string? ImageUrl { get; set; }

        public string? UserId { get; set; }

        public string[] SongIds { get; set; }
    }
}