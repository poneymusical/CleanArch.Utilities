using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArch.Utilities.Core.Service;
using CleanArch.Utilities.GenericCrud.Repository;
using CleanArch.Utilities.GenericCrud.Services.Update;
using Moq;
using NUnit.Framework;
using Test.CleanArch.Utilities.GenericCrud._TestArtifacts;

namespace Test.CleanArch.Utilities.GenericCrud.Services.Update
{
    public class TestUpdateHandler
    {
        private readonly IMapper _mapper;

        public TestUpdateHandler()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperConfiguration>());
            _mapper = new Mapper(mapperConfiguration);
        }

        [Test]
        public async Task Handle_EntityDoesNotExists()
        {
            var entity = new MockEntity
            {
                Id = Guid.NewGuid(),
                Value = "test"
            };

            var request = new MockUpdateRequest { Id = entity.Id, Value = "edited"};
            var repository = new Mock<IGenericRepository<MockEntity, Guid>>();
            repository.Setup(x => x.FindAsync(entity.Id))
                .ReturnsAsync((MockEntity)null);
            repository.Setup(x => x.UpdateAsync(entity))
                .ReturnsAsync(entity);

            var handler = new UpdateHandler<MockUpdateRequest, MockEntity, Guid>(repository.Object, _mapper);

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.AreEqual(ServiceResponseStatus.NotFound, response.Status);
            Assert.AreEqual(null, response.Payload);

            repository.Verify(x => x.FindAsync(entity.Id), Times.Once);
            repository.Verify(x => x.UpdateAsync(entity), Times.Never);
        }

        [Test]
        public async Task Handle_Ok()
        {
            var entity = new MockEntity
            {
                Id = Guid.NewGuid(),
                Value = "test"
            };

            var request = new MockUpdateRequest { Id = entity.Id, Value = "edited" };
            var repository = new Mock<IGenericRepository<MockEntity, Guid>>();
            repository.Setup(x => x.FindAsync(entity.Id))
                .ReturnsAsync(entity);
            repository.Setup(x => x.UpdateAsync(entity))
                .ReturnsAsync(entity);

            var handler = new UpdateHandler<MockUpdateRequest, MockEntity, Guid>(repository.Object, _mapper);

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.AreEqual(ServiceResponseStatus.Ok, response.Status);
            Assert.AreEqual(entity, response.Payload);
            Assert.AreEqual(request.Value, entity.Value);

            repository.Verify(x => x.FindAsync(entity.Id), Times.Once);
            repository.Verify(x => x.UpdateAsync(entity), Times.Once);
        }
    }
}