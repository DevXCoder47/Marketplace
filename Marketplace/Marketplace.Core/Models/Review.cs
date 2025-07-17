using Marketplace.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Core.Models
{
    public class Review : IEntity<string>
    {
        public Review()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; set; }
        public int Rating { get; set; }
        [Required]
        public string Text { get; set; } = null!;
    }
}
