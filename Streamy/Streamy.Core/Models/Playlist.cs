using System.ComponentModel.DataAnnotations;

namespace Streamy.Core.Models
{
    public class Playlist
    {
        public Playlist()
        {
            Id = Guid.NewGuid().ToString();
            Songs = new HashSet<SongPlaylist>();
        }

        [Key]
        public string Id { get; set; }

        public string Title { get; set; }

        public virtual ICollection<SongPlaylist> Songs { get; set; }
    }
}