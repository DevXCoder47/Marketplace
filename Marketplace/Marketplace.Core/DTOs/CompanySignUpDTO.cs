namespace Marketplace.Core.DTOs
{
    public class CompanySignUpDTO
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TaxNumber { get; set; } = null!;
        public string RegNumber { get; set; } = null!;
        public string CompanyEmail { get; set; } = null!;
        public string CompanyPassword { get; set; } = null!;
    }
}
