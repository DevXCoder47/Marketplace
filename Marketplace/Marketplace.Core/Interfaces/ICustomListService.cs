using Marketplace.Core.Models;

namespace Marketplace.Core.Interfaces
{
    public interface ICustomListService
    {
        public Task<CustomList> GetProductList(int skip, int take);
        public Task<CustomList> GetImageList(int skip, int take);
    }
}
