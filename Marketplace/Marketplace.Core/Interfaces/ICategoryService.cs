using Marketplace.Core.Models;

namespace Marketplace.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories(int skip, int take);
        Task<Category> GetCategoryById(string id);
        Task<Category> GetCategoryByName(string name);
        Task<Category> AddCategory(Category category);
        Task<Category> UpdateCategory(string id, Category category);
        Task DeleteCategory(string id);
    }
}
