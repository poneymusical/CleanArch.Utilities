using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArch.Utilities.Core.Service;
using CleanArch.Utilities.GenericCrud.Repository;
using CleanArch.Utilities.GenericCrud.Services.ReadPaginated;
using Moq;
using NUnit.Framework;
using Test.CleanArch.Utilities.GenericCrud._TestArtifacts;

namespace Test.CleanArch.Utilities.GenericCrud.Services.ReadPaginated
{
    public class TestReadPaginatedHandler
    {
        [Test]
        public async Task Handle()
        {
            var results = new[]
            {
                new MockEntity{Id = Guid.NewGuid(), Value = "test1"},
                new MockEntity{Id = Guid.NewGuid(), Value = "test2"}
            };

            var request = new MockReadPaginatedRequest
            {
                PageSize = 10,
                PageIndex = 0
            };

            var repository = new Mock<IGenericRepository<MockEntity, Guid>>();
            repository.Setup(x => x.GetPageAsync(request.PageIndex, request.PageSize, request.AddWhereConditions))
                .ReturnsAsync((int pageIndex, int pageSize, Func<IQueryable<MockEntity>, IQueryable<MockEntity>> addWhereConditions) =>
                {
                    if (addWhereConditions != request.AddWhereConditions)
                        throw new Exception("WhereCondition wasn't passed properly!");
                    return results;
                });

            var handler = new ReadPaginatedHandler<MockReadPaginatedRequest, MockEntity, Guid>(repository.Object);

            ServiceResponse<IEnumerable<MockEntity>> response = await handler.Handle(request, CancellationToken.None);
            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResponseStatus.Ok, response.Status);
            Assert.AreEqual(results, response.Payload);
        }
    }
}