using Marketplace.Core.Interfaces;
using Marketplace.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Core.Services
{
    public class MerchantService : IMerchantService
    {
        private readonly IRepository _repository;
        public MerchantService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Merchant>> GetMerchants(int skip, int take)
        {
            return await _repository.GetAll<Merchant>().
                Include(m => m.Products).
                Skip(skip).
                Take(take).
                ToListAsync();
        }

        public async Task<Merchant> GetMerchantById(string id)
        {
            var merchant = await _repository.GetByIdQueryable<Merchant>(id).
                Include(m => m.Products).
                FirstOrDefaultAsync();

            if (merchant == null)
                throw new ArgumentException($"Merchant with id {id} not found");    

            return merchant;
        }

        public async Task<Merchant> GetMerchantByName(string name)
        {
            var merchant = await _repository.GetAll<Merchant>().
            Include(m => m.Products).
            SingleOrDefaultAsync(m => m.Name.Equals(name));

            if (merchant == null)
                throw new ArgumentException("Merchant not found");

            return merchant;
        }

        public async Task<Merchant> AddMerchant(Merchant merchant)
        {
            return await _repository.Add(merchant);
        }

        public async Task DeleteMerchant(string id)
        {
            await _repository.Delete<Merchant>(id);
        }

        public async Task<Merchant> UpdateMerchant(string id, Merchant merchant)
        {
            return await _repository.Update(merchant, id);
        }
    }
}
