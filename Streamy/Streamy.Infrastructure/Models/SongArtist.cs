using System.ComponentModel.DataAnnotations.Schema;

namespace Streamy.Infrastructure.Models
{
    public class SongArtist
    {
        public string SongId { get; set; }

        public virtual Song Song { get; set; }


        public string ArtistId { get; set; }

        public virtual Artist Artist { get; set; }
    }
}