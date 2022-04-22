using Streamy.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public short GenreId { get; set; }

        public List<Artist> Artists { get; set; } = new List<Artist>();
        public List<Album> Albums { get; set; } = new List<Album>();
    }
}
