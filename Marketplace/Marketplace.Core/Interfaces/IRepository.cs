using Microsoft.EntityFrameworkCore;

namespace Marketplace.Core.Interfaces
{
    public interface IRepository
    {
        DbSet<T> GetAll<T>() where T : class;
        public Task<T> Add<T>(T entity) where T : class;
        public Task<T> Update<T>(T entity, int id) where T : class, IEntity;
        public Task<T?> GetByIdAsync<T>(int id) where T : class, IEntity;
        IQueryable<T> GetByIdQueryable<T>(int id) where T : class, IEntity;
        public Task Delete<T>(int id) where T : class, IEntity;
    }
}
