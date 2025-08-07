using Marketplace.Core.Interfaces;
using Marketplace.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository _repository;
        public CategoryService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Category>> GetCategories(int skip, int take)
        {
            return await _repository.GetAll<Category>()
                            .Skip(skip)
                            .Take(take)
                            .ToListAsync();
        }

        public async Task<Category> AddCategory(Category category)
        {
            return await _repository.Add(category); 
        }

        public async Task DeleteCategory(string id)
        {
            await _repository.Delete<Category>(id);
        }

        public async Task<Category> GetCategoryById(string id)
        {
            var category = await _repository.GetByIdAsync<Category>(id);

            if (category == null)
                throw new ArgumentException($"Category with id {id} not found");

            return category;
        }

        public async Task<Category> UpdateCategory(string id, Category category)
        {
            return await _repository.Update(category, id);
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            var category = await _repository.GetAll<Category>()
            .SingleOrDefaultAsync(c => c.Name.Equals(name));

            if (category == null)
                throw new ArgumentException("Category not found");

            return category;
        }
    }
}
