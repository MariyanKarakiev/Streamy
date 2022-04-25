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
        public AlbumViewModel Album { get; set; }

        public short GenreId { get; set; }
        public GenreViewModel Genre { get; set; }
         
        public GenreListViewModel GenreListViewModel { get; set; } = new GenreListViewModel();
        public AlbumListViewModel AlbumListViewModel { get; set; } = new AlbumListViewModel();
        public ArtistListViewModel ArtistListViewModel { get; set; } = new ArtistListViewModel();

        public List<ArtistViewModel> ArtistList { get; set; } = new List<ArtistViewModel>();
        public List<PlaylistViewModel> PlaylistList { get; set; } = new List<PlaylistViewModel>();
    }
}
