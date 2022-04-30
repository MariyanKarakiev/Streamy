using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Streamy.Core.Models
{
    public class PlaylistModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Songs are required")]
        public string[] SongIds { get; set; }


        [Required(ErrorMessage = "Image is required")]
        public IFormFile Image { get; set; }

        public string? ImageUrl { get; set; }
        public string? UserId { get; set; }
        public List<SongModel>? Songs { get; set; }
    }
}