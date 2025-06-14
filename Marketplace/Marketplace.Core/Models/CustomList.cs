namespace Marketplace.Core.Models
{
    public class CustomList
    {
        public int Count { get; set; }
        public ICollection<object>? Results { get; set; }
    }
}
