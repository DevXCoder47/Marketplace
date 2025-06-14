using Marketplace.Core.Models;

namespace Marketplace.Core.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetProducts(int skip, int take);
        public Task<Product> CreateProduct(Product product);
        public Task<Product> GetProductById(string id);
        public Task<Product> GetProductByName(string name);
        public Task<IEnumerable<Product>> GetProductsByName(string name, int skip, int take);
        public Task DeleteProduct(string id);
    }
}
