using Marketplace.Core.Interfaces;

namespace Marketplace.Core.Models
{
    public class RefreshToken : IEntity<string>
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
