using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamy.Infrastructure.Models
{
    public class Song
    {
        public Song()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }
        public TimeSpan Duration { get; set; }

        [Required]
        public short GenreId { get; set; }
        public Genre Genre { get; set; }

        public string AlbumId { get; set; }
        public Album Album { get; set; }

        public virtual ICollection<SongArtist> Artists { get; set; }
        public virtual ICollection<SongPlaylist> Playlists { get; set; }

    }
}
