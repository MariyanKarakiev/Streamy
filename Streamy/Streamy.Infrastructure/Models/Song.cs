using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Streamy.Infrastructure.Models
{
    public class Song
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }
        public TimeSpan Duration { get; set; }

        [ForeignKey(nameof(Album))]
        public Guid? AlbumId { get; set; }
        public Album? Album { get; set; }

        [Required]
        [ForeignKey(nameof(Genre))]
        public short GenreId { get; set; }
        public Genre Genre { get; set; }

        public virtual ICollection<SongArtist> Artists { get; set; } = new HashSet<SongArtist>();
        public virtual ICollection<SongPlaylist> Playlists { get; set; } = new HashSet<SongPlaylist>();

    }
}
