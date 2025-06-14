using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Core.DTOs
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public string? Descriprtion { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }

        //Продумать, как сделать, что-бы можно было задать сразу несколько категорий тут же
        //public ICollection<CategoryDTO>? Categories { get; set; }
    }
}
