using Marketplace.Core.Helpers;
using Marketplace.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Marketplace.Core.Models
{
    public class Company : IEntity<string>
    {
        public Company()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TaxNumber { get; set; } = null!;
        public string RegNumber { get; set; } = null!;
        public string CompanyEmail { get; set; } = null!;
        public string CompanyPassword { get; set; } = null!;
        public OnlineStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}
