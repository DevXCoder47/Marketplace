using Marketplace.Core.Helpers;

namespace Marketplace.Core.DTOs
{
    public class CompanyDTO
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TaxNumber { get; set; } = null!;
        public string RegNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public OnlineStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
