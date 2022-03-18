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
        [Key]
        public short Id { get; set; }
        public string? Name { get; set; }
    }
}
