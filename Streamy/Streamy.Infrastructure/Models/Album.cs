using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Streamy.Infrastructure.Models
{
    public class Album
    {
        public Album()
        {
            Id = Guid.NewGuid().ToString();
            //  Artists = new HashSet<SongArtist>();
            Songs = new HashSet<Song>();
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


        public virtual ICollection<Song> Songs { get; set; }
        // public virtual ICollection<SongArtist> Artists { get; set; }

    }
}