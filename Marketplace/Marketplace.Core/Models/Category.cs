using System.ComponentModel.DataAnnotations;
using Marketplace.Core.Interfaces;

namespace Marketplace.Core.Models
{
    public class Category : IEntity<string>
    {
        public Category()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
    }
}
