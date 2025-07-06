using Marketplace.Core.Models;

namespace Marketplace.Core.Interfaces
{
    public interface ICustomListService
    {
        Task<CustomList> GetUserList(int skip, int take);
        Task<CustomList> GetProductList(int skip, int take);
        Task<CustomList> GetImageList(int skip, int take);
    }
}
