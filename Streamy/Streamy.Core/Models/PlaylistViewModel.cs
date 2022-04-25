using System.ComponentModel.DataAnnotations;

namespace Streamy.Core.Models
{
    public class PlaylistViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(70)]
        public string Title { get; set; }
    }
}