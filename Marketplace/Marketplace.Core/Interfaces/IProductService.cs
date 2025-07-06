using Marketplace.Core.Models;

namespace Marketplace.Core.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts(int skip, int take);
        Task<Product> CreateProduct(Product product);
        Task<Product> GetProductById(string id);
        Task<Product> GetProductByName(string name);
        Task<IEnumerable<Product>> GetProductsByName(string name, int skip, int take);
        Task DeleteProduct(string id);
    }
}
