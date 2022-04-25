using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamy.Core.Models.Song
{
    public class SongCreateModel : SongModel
    {
        public List<GenreViewModel> Genres { get; set; } = new List<GenreViewModel>();
        public List<AlbumViewModel> Albums { get; set; } = new List<AlbumViewModel>();
        public List<ArtistViewModel> AllArtists{ get; set; } = new List<ArtistViewModel>();
        public List<PlaylistViewModel> AllPlaylists { get; set; } = new List<PlaylistViewModel>();
    }
}
