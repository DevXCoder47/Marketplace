using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Marketplace.Core.Interfaces;

namespace Marketplace.Core.Models
{
    public class Image : IEntity<string>
    {
        public Image()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; set; }
        public string FilePath { get; set; }
        public string AltText { get; set; }
        [ForeignKey(nameof(Product))]
        public string? ProductId { get; set; }
        public Product Product { get; set; }
    }
}
