namespace Marketplace.Core.DTOs
{
    public class AddImageDTO
    {
        public string FilePath { get; set; }
        public string AltText { get; set; }
        public int? ProductId { get; set; }
    }
}
