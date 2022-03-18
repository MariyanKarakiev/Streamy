using System.ComponentModel.DataAnnotations.Schema;

namespace Streamy.Core.Models
{
    public class SongAlbum
    {
        [ForeignKey(nameof(Song))]
        public int SongId { get; set; }

        public virtual Song Song { get; set; }


        [ForeignKey(nameof(Album))]
        public int AlbumId { get; set; }

        public virtual Album Album { get; set; }
    }
}