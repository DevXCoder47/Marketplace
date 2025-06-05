using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Core.DTOs
{
    public class UserLoginDTO
    {
        public string Nickname { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
