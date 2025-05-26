using Marketplace.Core.Models;

namespace Marketplace.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories(int skip, int take);
        Task<Category> GetCategoryById(int id);
        Task<Category> GetCategoryByName(string name);
        Task<Category> AddCategory(Category category);
        Task<Category> UpdateCategory(int id, Category category);
        Task DeleteCategory(int id);
    }
}
