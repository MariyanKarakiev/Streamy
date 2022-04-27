using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamy.Core.Models.Song
{
    public class SongCreateModel : SongModel
    {
        public string[]? ArtistIds { get; set; }
    }
}
