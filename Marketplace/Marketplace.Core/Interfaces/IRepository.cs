using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Core.Interfaces
{
    public interface IRepository
    {
        DbSet<T> GetAll<T>() where T : class;
        public Task<T> Add<T>(T entity) where T : class;
        public Task<T> Update<T>(T entity, int id) where T : class;
        public Task<T?> GetById<T>(int id) where T : class;
        public Task Delete<T>(int id) where T : class;
    }
}
