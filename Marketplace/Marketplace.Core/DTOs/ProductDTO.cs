namespace Marketplace.Core.DTOs
{
    public class ProductDTO
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string? Descriprtion { get; set; }
        public float? Rating { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
        
        //Если имеджи поимеют шорт-продукт-дто, то здесь тоже надо заменить на шорт-имедж-дто
        public ICollection<ImageDTO>? Images { get; set; }
        //Аналогично
        public ICollection<CategoryDTO>? Categories { get; set; }
    }
}
