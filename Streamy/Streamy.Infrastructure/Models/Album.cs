using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Streamy.Infrastructure.Models
{
    public class Album
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Column(TypeName = "date")]
        public DateTime ReleaseDate { get; set; }

        public TimeSpan Duration { get; set; }

        [Required]
        [ForeignKey(nameof(Genre))]
        public short GenreId { get; set; }
        public Genre Genre { get; set; }

        public virtual ICollection<Song> Songs { get; set; } = new HashSet<Song>();

    }
}