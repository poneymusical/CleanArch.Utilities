using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArch.Utilities.Core.Service;
using CleanArch.Utilities.GenericCrud.Repository;
using CleanArch.Utilities.GenericCrud.Services.ReadSingle;
using Moq;
using NUnit.Framework;
using Test.CleanArch.Utilities.GenericCrud._TestArtifacts;

namespace Test.CleanArch.Utilities.GenericCrud.Services.ReadSingle
{
    public class TestReadSingleHandler
    {
        [Test]
        public async Task Handle_EntityDoesNotExists()
        {
            var entity = new MockEntity
            {
                Id = Guid.NewGuid(),
                Value = "test"
            };

            var request = new ReadSingleRequest<MockEntity, Guid> { Id = entity.Id };
            var repository = new Mock<IGenericRepository<MockEntity, Guid>>();
            repository.Setup(x => x.FindAsync(entity.Id))
                .ReturnsAsync((MockEntity)null);

            var handler = new ReadSingleHandler<ReadSingleRequest<MockEntity, Guid>, MockEntity, Guid>(repository.Object);
            var response = await handler.Handle(request, CancellationToken.None);

            Assert.AreEqual(ServiceResponseStatus.NotFound, response.Status);
            Assert.IsNull(response.Payload);

            repository.Verify(x => x.FindAsync(entity.Id), Times.Once);
        }

        [Test]
        public async Task Handle_Ok()
        {
            var entity = new MockEntity
            {
                Id = Guid.NewGuid(),
                Value = "test"
            };

            var request = new ReadSingleRequest<MockEntity, Guid> { Id = entity.Id };
            var repository = new Mock<IGenericRepository<MockEntity, Guid>>();
            repository.Setup(x => x.FindAsync(entity.Id))
                .ReturnsAsync(entity);

            var handler = new ReadSingleHandler<ReadSingleRequest<MockEntity, Guid>, MockEntity, Guid>(repository.Object);
            var response = await handler.Handle(request, CancellationToken.None);

            Assert.AreEqual(ServiceResponseStatus.Ok, response.Status);
            Assert.AreEqual(entity, response.Payload);

            repository.Verify(x => x.FindAsync(entity.Id), Times.Once);
        }
    }
}