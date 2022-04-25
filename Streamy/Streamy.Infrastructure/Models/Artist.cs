using System.ComponentModel.DataAnnotations;

namespace Streamy.Infrastructure.Models
{
    public class Artist
    {
        public Guid Id { get; set; } = Guid.NewGuid();
      
        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        [Required]
        [StringLength(56)]
        public string Country { get; set; }

        public virtual ICollection<Song> Songs { get; set; } = new HashSet<Song>();
        public virtual ICollection<Album> Albums { get; set; } = new HashSet<Album>();

    }
}