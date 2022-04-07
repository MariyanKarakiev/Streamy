using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.ComponentModel.DataAnnotations;


namespace Streamy.Core.Models
{
    public class GenreModel
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
