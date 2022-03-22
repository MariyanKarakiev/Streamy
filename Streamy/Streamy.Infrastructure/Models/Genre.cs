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
        public string Name { get; set; }     
    }
}
