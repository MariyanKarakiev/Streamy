using System.ComponentModel.DataAnnotations;

namespace Streamy.Infrastructure.Models
{
    public class Artist
    {
        public Artist()
        {
            Id = Guid.NewGuid().ToString();
            Songs = new HashSet<SongArtist>();
        }

        public string Id { get; set; }
      
        [Required]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }

        public virtual ICollection<SongArtist> Songs { get; set; }
    }
}