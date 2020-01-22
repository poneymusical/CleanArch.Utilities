using System.Threading.Tasks;
using CleanArch.Utilities.GenericCrud.Entities;

namespace CleanArch.Utilities.GenericCrud.Repository
{
    public interface IGenericRepository<TEntity, TId> 
        where TEntity : IIdentifiable<TId>
    {
        Task<TEntity> CreateAsync(TEntity entity);
    }
}