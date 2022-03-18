using System.ComponentModel.DataAnnotations.Schema;

namespace Streamy.Core.Models
{
    public class SongArtist
    {
        [ForeignKey(nameof(Song))]
        public int SongId { get; set; }

        public virtual Song Song { get; set; }


        [ForeignKey(nameof(Artist))]
        public int ArtistId { get; set; }

        public virtual Artist Artist { get; set; }
    }
}