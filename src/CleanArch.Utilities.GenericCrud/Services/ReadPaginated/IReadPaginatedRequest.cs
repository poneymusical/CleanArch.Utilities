using System.Collections.Generic;
using System.Linq;
using CleanArch.Utilities.Core.Service;
using CleanArch.Utilities.GenericCrud.Entities;

namespace CleanArch.Utilities.GenericCrud.Services.ReadPaginated
{
    public interface IReadPaginatedRequest<TEntity, TId> : IServiceRequest<IEnumerable<TEntity>>
        where TEntity : class, IIdentifiable<TId>
    {
        int PageIndex { get; set; }
        int PageSize { get; set; }
        IQueryable<TEntity> AddWhereConditions(IQueryable<TEntity> queryable);
    }
}