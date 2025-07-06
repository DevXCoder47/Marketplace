using Marketplace.Core.Models;

namespace Marketplace.Core.DTOs
{
    public class UserDataDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<string> Roles {  get; set; }
    }
}
