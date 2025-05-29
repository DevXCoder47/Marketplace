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
        Task<Merchant> GetMerchantById(int id);
        Task<Merchant> GetMerchantByName(string name);
        Task<Merchant> AddMerchant(Merchant merchant);
        Task<Merchant> UpdateMerchant(int id, Merchant merchant);
        Task DeleteMerchant(int id);
    }
}
