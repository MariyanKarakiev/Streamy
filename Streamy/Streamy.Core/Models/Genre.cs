using Streamy.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamy.Core.Enums
{
    public class Genre
    {
        public Genre()
        {
            Songs = new HashSet<Song>();
        }

        [Key]
        public short Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}
