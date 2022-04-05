using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamy.Core.Models
{
    public class GenreModel
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
