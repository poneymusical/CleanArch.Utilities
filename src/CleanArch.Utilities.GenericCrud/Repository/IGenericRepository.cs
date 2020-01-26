using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArch.Utilities.GenericCrud.Entities;

namespace CleanArch.Utilities.GenericCrud.Repository
{
    public interface IGenericRepository<TEntity, TId> 
        where TEntity : class, IIdentifiable<TId>
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> FindAsync(TId id);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetPageAsync(int pageIndex, int pageSize, Func<IQueryable<TEntity>, IQueryable<TEntity>> where);
        Task<TId> DeleteAsync(TEntity entity);
    }
}