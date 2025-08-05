using System.ComponentModel.DataAnnotations;

namespace Marketplace.Core.DTOs
{
    public class ReviewDTO
    {
        [Required]
        public int Rating { get; set; }
        [Required]
        public string Text { get; set; } = null!;
    }
}
