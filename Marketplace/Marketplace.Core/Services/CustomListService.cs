using Marketplace.Core.Interfaces;
using Marketplace.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Core.Services
{
    public class CustomListService : ICustomListService
    {
        private readonly IRepository _repository;
        public CustomListService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomList> GetImageList(int skip, int take)
        {
            var customlist = new CustomList();
            var temp = await _repository.GetAll<Image>().
                Skip(skip).
                Take(take).
                ToListAsync();

            ICollection<object> collection = temp.Cast<object>().ToList();
            customlist.Results = collection;
            customlist.Count = collection.Count;

            return customlist;
        }

        public async Task<CustomList> GetProductList(int skip, int take)
        {
            var customlist = new CustomList();
            var temp = await _repository.GetAll<Product>().
                Skip(skip).
                Take(take).
                ToListAsync();

            ICollection<object> collection = temp.Cast<object>().ToList();
            customlist.Results = collection;
            customlist.Count = collection.Count;

            return customlist;
        }
    }
}

