using Marketplace.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Core.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Nickname { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public OnlineStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
