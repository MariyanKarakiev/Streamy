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

        [Column(TypeName = "date")]
        public DateTime ReleaseDate { get; set; }
        public TimeSpan Duration { get; set; }

        public Guid? AlbumId { get; set; }

        [ForeignKey(nameof(AlbumId))]
        public Album? Album { get; set; }

        [Required]
        public short GenreId { get; set; }

        [ForeignKey(nameof(GenreId))]
        public Genre Genre { get; set; }

        public virtual ICollection<Artist> Artists { get; set; } = new HashSet<Artist>();
        public virtual ICollection<Playlist> Playlists { get; set; } = new HashSet<Playlist>();

    }
}
