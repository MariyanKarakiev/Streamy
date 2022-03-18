using Streamy.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamy.Core.Models
{
    public class Song
    {
        public Song()
        {
            Id = Guid.NewGuid().ToString();
            Artists = new HashSet<SongArtist>();
            Playlists = new HashSet<SongPlaylist>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }
        public TimeSpan Duration { get; set; }


        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }


        [ForeignKey(nameof(Album))]
        public int AlbumId { get; set; }
        public Genre Album { get; set; }

        public virtual ICollection<SongArtist> Artists { get; set; }
        public virtual ICollection<SongPlaylist> Playlists { get; set; }

    }
}
