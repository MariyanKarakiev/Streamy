using System.ComponentModel.DataAnnotations;

namespace Streamy.Infrastructure.Models
{
    public class Playlist
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(70)]
        public string Title { get; set; }

        public virtual ICollection<SongPlaylist> Songs { get; set; } = new HashSet<SongPlaylist>();
    }
}