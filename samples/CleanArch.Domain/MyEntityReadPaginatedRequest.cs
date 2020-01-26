using System.Linq;
using CleanArch.Utilities.GenericCrud.Services.ReadPaginated;

namespace CleanArch.Domain
{
    public class MyEntityReadPaginatedRequest : ReadPaginatedRequest<MyEntity, int>
    {
        public MyEntityFilter Filter { get; set; }

        public override IQueryable<MyEntity> AddWhereConditions(IQueryable<MyEntity> queryable)
        {
            if (Filter == null)
                return queryable;

            if (!string.IsNullOrWhiteSpace(Filter.Value))
                queryable = queryable.Where(x => x.Value.ToLowerInvariant().Contains(Filter.Value.ToLowerInvariant()));

            return queryable;
        }
    }

    public class MyEntityFilter
    {
        public string Value { get; set; }
    }
}
