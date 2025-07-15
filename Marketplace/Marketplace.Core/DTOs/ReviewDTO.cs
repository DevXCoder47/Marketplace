using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Core.DTOs
{
    public class ReviewDTO
    {
        [Required]
        public string Text { get; set; } = null!;
    }
}
