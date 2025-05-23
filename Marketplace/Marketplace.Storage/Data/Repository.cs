using Marketplace.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Storage.Data
{
    public class Repository : IRepository
    {
        private readonly MarketplaceContext _MarketplaceContext;
        public Repository(MarketplaceContext _MarketplaceContext)
        {
            this._MarketplaceContext = _MarketplaceContext;
        }

        public DbSet<T> GetAll<T>() where T : class
        {
            return _MarketplaceContext.Set<T>();
        }

        public async Task<T> Add<T>(T entity) where T : class
        {
            _MarketplaceContext.Add(entity);
            await _MarketplaceContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete<T>(int id) where T : class
        {
            var entity = await GetById<T>(id);

            if (entity == null)
            {
                throw new ArgumentException($"Entity with ID: {id} not found.");
            }

            _MarketplaceContext.Set<T>().Remove(entity);
            await _MarketplaceContext.SaveChangesAsync();
        }

        public async Task<T?> GetById<T>(int id) where T : class
        {
            var entity = await _MarketplaceContext.Set<T>().FindAsync(id);

            if (entity == null)
            {
                throw new ArgumentException($"Entity with ID: {id} not found.");
            }

            return entity;
        }

        public async Task<T> Update<T>(T entity, int id) where T : class
        {
            var selectedEntity = await GetById<T>(id);

            if (selectedEntity == null)
            {
                throw new ArgumentException($"Entity with ID: {id} not found.");
            }

            _MarketplaceContext.Entry(selectedEntity).CurrentValues.SetValues(entity);
            await _MarketplaceContext.SaveChangesAsync();
            return entity;
        }
    }
}
