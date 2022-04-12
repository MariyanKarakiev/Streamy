using Streamy.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamy.Infrastructure.Models
{
    public class Genre
    {
        [Key]
        public short Id { get; set; }

        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        public virtual ICollection<Album> Albums { get; set; } = new HashSet<Album>();
        public virtual ICollection<Song> Songs { get; set; } = new HashSet<Song>();
    }
}
