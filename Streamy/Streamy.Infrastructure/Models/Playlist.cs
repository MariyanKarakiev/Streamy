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
      
        [StringLength(80)]
        public string ImageUrl { get; set; }
        
        [Required]
        [StringLength(450)]
        public string UserId { get; set; }


        public virtual ICollection<Song> Songs { get; set; } = new HashSet<Song>();
    }
}