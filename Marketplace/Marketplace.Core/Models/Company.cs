using Marketplace.Core.Helpers;
using Marketplace.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Core.Models
{
    public class Company : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TaxNumber { get; set; } = null!;
        public string RegNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public OnlineStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
