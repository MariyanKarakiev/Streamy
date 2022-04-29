using System.ComponentModel.DataAnnotations;

namespace Streamy.Core.Models
{
    public class ArtistModel
    {
        public string? Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(56, MinimumLength = 2)]
        public string Country { get; set; }

    }
}