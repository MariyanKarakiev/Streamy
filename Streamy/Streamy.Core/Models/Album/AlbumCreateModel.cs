using System.ComponentModel.DataAnnotations;

namespace Streamy.Core.Models
{
    public class AlbumCreateModel 
    {
        public AlbumModel Album { get; set; }

        [Required]
        public string[] SongIds { get; set; }

    }
}