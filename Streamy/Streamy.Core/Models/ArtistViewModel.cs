using System.ComponentModel.DataAnnotations;

namespace Streamy.Core.Models
{
    public class ArtistViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        [Required]
        [StringLength(56)]
        public string Country { get; set; }
    }
}