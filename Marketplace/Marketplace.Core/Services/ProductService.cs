using Marketplace.Core.DTOs;
using Marketplace.Core.Interfaces;
using Marketplace.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository _repository;
        public ProductService(IRepository repository)
        {
            _repository = repository;
        }

        public Task<Product> CreateProduct(Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
                throw new ArgumentException("Invalid product build");  // TODO own types of exceptions
            return _repository.Add(product);
        }

        public async Task DeleteProduct(string id)
        {
            await _repository.Delete<Product>(id);
        }

        public async Task<IEnumerable<Product>> GetFilteredProducts(FilterDTO filter, int skip, int take)
        {
            var query = _repository.GetAll<Product>()
                .Include(p => p.Images)
                .Include(p => p.Categories)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(p => p.Name.Contains(filter.Name));
            }

            if (!string.IsNullOrEmpty(filter.Descriprtion))
            {
                query = query.Where(p => p.Descriprtion != null && p.Descriprtion.Contains(filter.Descriprtion));
            }

            if (filter.Rating.HasValue)
            {
                query = query.Where(p => p.Rating.HasValue && p.Rating.Value >= filter.Rating.Value);
            }

            if (filter.Price.HasValue)
            {
                query = query.Where(p => p.Price <= filter.Price.Value);
            }

            return await query
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            var product = _repository.GetByIdQueryable<Product>(id).
                Include(p => p.Images).
                Include(p => p.Categories).
                FirstOrDefaultAsync();

            if (product == null)
                throw new ArgumentException("Product not found");    // TODO own types of exceptions

            return await product;
        }

        public async Task<Product> GetProductByName(string name)
        {
            var product = await _repository.GetAll<Product>()
                .SingleOrDefaultAsync(p => p.Name.Equals(name));

            if (product == null)
            {
                throw new ArgumentException("Product not found");
            }

            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts(int skip, int take)
        {
            return await _repository.GetAll<Product>().
                Include(t => t.Images).
                Include(t => t.Categories).
                Skip(skip).
                Take(take).
                ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name, int skip, int take)
        {
            return await _repository.GetAll<Product>()
                .Where(t => t.Name.Contains(name))
                .Skip(skip)
                .Take(take)
                .ToArrayAsync();
        }
    }
}
