using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArch.Utilities.Core.Service;
using CleanArch.Utilities.GenericCrud.Repository;
using CleanArch.Utilities.GenericCrud.Services.Delete;
using Moq;
using NUnit.Framework;
using Test.CleanArch.Utilities.GenericCrud._TestArtifacts;

namespace Test.CleanArch.Utilities.GenericCrud.Services.Delete
{
    public class TestDeleteHandler
    {
        [Test]
        public async Task Handle_EntityDoesNotExists()
        {
            var entity = new MockEntity
            {
                Id = Guid.NewGuid(),
                Value = "test"
            };

            var request = new DeleteRequest<MockEntity, Guid> { Id = entity.Id };
            var repository = new Mock<IGenericRepository<MockEntity, Guid>>();
            repository.Setup(x => x.FindAsync(entity.Id))
                .ReturnsAsync((MockEntity)null);
            repository.Setup(x => x.DeleteAsync(entity))
                .ReturnsAsync((MockEntity ent) => ent.Id);

            var handler = new DeleteHandler<DeleteRequest<MockEntity, Guid>, MockEntity, Guid>(repository.Object);
            var response = await handler.Handle(request, CancellationToken.None);

            Assert.AreEqual(ServiceResponseStatus.NotFound, response.Status);

            repository.Verify(x => x.FindAsync(entity.Id), Times.Once);
            repository.Verify(x => x.DeleteAsync(entity), Times.Never);
        }

        [Test]
        public async Task Handle_Ok()
        {
            var entity = new MockEntity
            {
                Id = Guid.NewGuid(),
                Value = "test"
            };

            var request = new DeleteRequest<MockEntity, Guid> { Id = entity.Id };
            var repository = new Mock<IGenericRepository<MockEntity, Guid>>();
            repository.Setup(x => x.FindAsync(entity.Id))
                .ReturnsAsync(entity);
            repository.Setup(x => x.DeleteAsync(entity))
                .ReturnsAsync((MockEntity ent) => ent.Id);

            var handler = new DeleteHandler<DeleteRequest<MockEntity, Guid>, MockEntity, Guid>(repository.Object);
            var response = await handler.Handle(request, CancellationToken.None);

            Assert.AreEqual(ServiceResponseStatus.Ok, response.Status);

            repository.Verify(x => x.FindAsync(entity.Id), Times.Once);
            repository.Verify(x => x.DeleteAsync(entity), Times.Once);
        }
    }
}