using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArch.Utilities.GenericCrud.Entities;
using CleanArch.Utilities.GenericCrud.Repository;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Utilities.Repository.EntityFrameworkCore
{
    public class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId> 
        where TEntity : class, IIdentifiable<TId>
    {
        private readonly DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> FindAsync(TId id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetPageAsync(int pageIndex, int pageSize, Func<IQueryable<TEntity>, IQueryable<TEntity>> where)
        {
            var query = _context.Set<TEntity>().Skip(pageIndex * pageSize);
            query = where(query);
            return await query.ToListAsync();
        }

        public async Task<TId> DeleteAsync(TEntity entity)
        {
            var id = entity.Id;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return id;
        }
    }
}