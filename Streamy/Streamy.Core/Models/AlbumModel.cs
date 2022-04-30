using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Streamy.Core.Models
{
    public class AlbumModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Release date is required")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm\\:ss}", ApplyFormatInEditMode = true)]
        public TimeSpan Duration { get; set; }
  
        [Required(ErrorMessage = "Artist is required")]
        public string ArtistId { get; set; }
        public ArtistModel? Artist { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public IFormFile Image { get; set; }
        public string? ImageUrl { get; set; }
        
        [Required(ErrorMessage = "Songs are required")]
        public string[] SongIds { get; set; }


        public string? UserId { get; set; }
        public List<SongModel>? Songs { get; set; }
    }
}