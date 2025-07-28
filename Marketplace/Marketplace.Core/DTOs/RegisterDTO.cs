namespace Marketplace.Core.DTOs
{
    public class RegisterDTO
    {
        public string Email { get; set; } = null!;
        public string Nickname { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
