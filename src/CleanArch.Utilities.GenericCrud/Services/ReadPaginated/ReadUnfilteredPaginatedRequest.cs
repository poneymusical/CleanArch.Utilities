using System.Linq;
using CleanArch.Utilities.GenericCrud.Entities;

namespace CleanArch.Utilities.GenericCrud.Services.ReadPaginated
{
    public class ReadUnfilteredPaginatedRequest<TEntity, TId> : IReadPaginatedRequest<TEntity, TId>
        where TEntity : class, IIdentifiable<TId>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public IQueryable<TEntity> AddWhereConditions(IQueryable<TEntity> queryable)
        {
            return queryable;
        }
    }
}
