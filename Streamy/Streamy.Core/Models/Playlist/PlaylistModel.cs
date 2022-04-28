using System.ComponentModel.DataAnnotations;

namespace Streamy.Core.Models
{
    public class PlaylistModel
    {
        public string? Id { get; set; }
        public string Title { get; set; }

        public string[] SongIds { get; set; }
        public List<SongModel>? Songs { get; set; }
    }
}