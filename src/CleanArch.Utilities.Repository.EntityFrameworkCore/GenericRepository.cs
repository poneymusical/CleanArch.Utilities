using System.Threading.Tasks;
using CleanArch.Utilities.GenericCrud.Entities;
using CleanArch.Utilities.GenericCrud.Repository;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Utilities.Repository.EntityFrameworkCore
{
    public class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId> 
        where TEntity : IIdentifiable<TId>
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
    }
}