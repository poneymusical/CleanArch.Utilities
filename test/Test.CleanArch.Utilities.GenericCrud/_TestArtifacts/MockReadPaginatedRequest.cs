using System;
using System.Linq;
using CleanArch.Utilities.GenericCrud.Services.ReadPaginated;

namespace Test.CleanArch.Utilities.GenericCrud._TestArtifacts
{
    public class MockReadPaginatedRequest : ReadPaginatedRequest<MockEntity, Guid>
    {
        public override IQueryable<MockEntity> AddWhereConditions(IQueryable<MockEntity> queryable) 
            => queryable;
    }
}