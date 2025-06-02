using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Core.DTOs
{
    public class UserModelDTO
    {
        public string Nickname { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
