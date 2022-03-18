using System.ComponentModel.DataAnnotations.Schema;

namespace Streamy.Infrastructure.Models
{
    public class SongPlaylist
    {
        [ForeignKey(nameof(Song))]
        public string SongId { get; set; }

        public virtual Song Song { get; set; }


        [ForeignKey(nameof(Playlist))]
        public string PlaylistId { get; set; }

        public virtual Playlist Playlist { get; set; }
    }
}