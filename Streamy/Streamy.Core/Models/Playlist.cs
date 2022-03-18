using System.ComponentModel.DataAnnotations;

namespace Streamy.Core.Models
{
    public class Playlist
    {
        public Playlist()
        {
            Songs = new HashSet<Song>();
        }

        [Key]
        public string Id { get; set; }

        public string Title { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}