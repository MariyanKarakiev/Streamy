namespace Streamy.Core.Models
{
    public class Artist
    {
        public Artist()
        {
            Id = Guid.NewGuid().ToString();
            Songs = new HashSet<SongArtist>();
        }

        public string Id { get; set; }
      
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Country { get; set; }

        public virtual ICollection<SongArtist> Songs { get; set; }
    }
}