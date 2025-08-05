using System.ComponentModel.DataAnnotations;

namespace Marketplace.Core.DTOs
{
    public class CategoryDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
    }
}
