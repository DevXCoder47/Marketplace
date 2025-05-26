using System.ComponentModel.DataAnnotations;
using Marketplace.Core.Interfaces;

namespace Marketplace.Core.Models
{
    public class Product : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Descriprtion { get; set; }
        public float? Rating { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
        public ICollection<Image>? Images { get; set; }
        public ICollection<Category>? Categories { get; set; }
    }
}
