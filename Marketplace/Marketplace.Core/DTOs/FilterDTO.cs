using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Core.DTOs
{
    public class FilterDTO
    {
        public string? Name { get; set; }
        public string? Descriprtion { get; set; }
        public float? Rating { get; set; }
        public float? Price { get; set; }
    }
}
