using System.ComponentModel.DataAnnotations.Schema;

namespace Streamy.Infrastructure.Models
{
    public class SongPlaylist
    {
        public string SongId { get; set; }

        public virtual Song Song { get; set; }


        public string PlaylistId { get; set; }

        public virtual Playlist Playlist { get; set; }
    }
}