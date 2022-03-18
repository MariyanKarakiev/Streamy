using System.ComponentModel.DataAnnotations.Schema;

namespace Streamy.Infrastructure.Models
{
    public class SongAlbum
    {
        [ForeignKey(nameof(Song))]
        public string SongId { get; set; }

        public virtual Song Song { get; set; }


        [ForeignKey(nameof(Album))]
        public string AlbumId { get; set; }

        public virtual Album Album { get; set; }
    }
}