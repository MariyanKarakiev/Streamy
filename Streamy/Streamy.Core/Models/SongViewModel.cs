using Streamy.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace Streamy.Core.Models
{
    public class SongViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [DataType(DataType.Duration)]
        public TimeSpan Duration { get; set; }

        public Guid? AlbumId { get; set; }
        public AlbumViewModel? Album { get; set; }

        public short GenreId { get; set; }
        public GenreViewModel? Genre { get; set; }
         

        public List<ArtistViewModel> ArtistList { get; set; } = new List<ArtistViewModel>();
        public List<Playlist> PlayLists { get; set; } = new List<Playlist>();
    }
}
