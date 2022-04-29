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
        public Guid ArtistId { get; set; }
       
        [ForeignKey(nameof(ArtistId))]
        public Artist Artist { get; set; }

        [StringLength(2083)]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(450)]
        public string UserId { get; set; }

        public virtual ICollection<Song> Songs { get; set; } = new HashSet<Song>();

    }
}