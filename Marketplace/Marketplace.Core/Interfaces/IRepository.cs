using Microsoft.EntityFrameworkCore;

namespace Marketplace.Core.Interfaces
{
    public interface IRepository
    {
        DbSet<T> GetAll<T>() where T : class;
        public Task<T> Add<T>(T entity) where T : class;
        public Task<T> Update<T>(T entity, string id) where T : class, IEntity<string>;
        public Task<T?> GetByIdAsync<T>(string id) where T : class, IEntity<string>;
        IQueryable<T> GetByIdQueryable<T>(string id) where T : class, IEntity<string>;
        public Task Delete<T>(string id) where T : class, IEntity<string>;
    }
}
