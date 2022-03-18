using Streamy.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Streamy.Core.Models
{
    public class Album
    {
        public Album()
        {
            Id = Guid.NewGuid().ToString();
            Artists = new HashSet<SongArtist>();
        }


        [Key]
        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public TimeSpan Duration { get; set; }

      
        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }


        public virtual ICollection<SongArtist> Artists { get; set; }

    }
}