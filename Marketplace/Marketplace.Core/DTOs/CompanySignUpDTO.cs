namespace Marketplace.Core.DTOs
{
    public class CompanySignUpDTO
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TaxNumber { get; set; } = null!;
        public string RegNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
