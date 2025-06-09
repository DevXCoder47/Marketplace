using Marketplace.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Core.Interfaces
{
    public interface IMerchantService
    {
        Task<IEnumerable<Merchant>> GetMerchants(int skip, int take);
        Task<Merchant> GetMerchantById(string id);
        Task<Merchant> GetMerchantByName(string name);
        Task<Merchant> AddMerchant(Merchant merchant);
        Task<Merchant> UpdateMerchant(string id, Merchant merchant);
        Task DeleteMerchant(string id);
    }
}
