using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.ComponentModel.DataAnnotations;


namespace Streamy.Core.Models
{
    public class GenreModel
    {
        public short Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }
        public int? Albums { get; set; }
        public int? Songs { get; set; }

        public string? UserId { get; set; }

    }
}
