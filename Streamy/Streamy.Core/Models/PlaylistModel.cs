using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Streamy.Core.Models
{
    public class PlaylistModel
    {
        public string? Id { get; set; }
        public string Title { get; set; }

        public string[] SongIds { get; set; }
        public List<SongModel>? Songs { get; set; }
        public string? UserId { get; set; }
        public IFormFile Image { get; set; }
        public string? ImageUrl { get; set; }
    }
}