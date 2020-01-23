using CleanArch.Utilities.GenericCrud.Entities;

namespace CleanArch.Utilities.GenericCrud.Services.ReadPaginated
{
    public class ReadPaginatedRequest<TEntity, TId> : IReadPaginatedRequest<TEntity, TId> 
        where TEntity : class, IIdentifiable<TId>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}