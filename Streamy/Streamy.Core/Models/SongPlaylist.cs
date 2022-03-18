using System.ComponentModel.DataAnnotations.Schema;

namespace Streamy.Core.Models
{
    public class SongPlaylist
    {
        [ForeignKey(nameof(Song))]
        public int SongId { get; set; }

        public virtual Song Song { get; set; }


        [ForeignKey(nameof(Playlist))]
        public int PlaylistId { get; set; }

        public virtual Playlist Playlist { get; set; }
    }
}