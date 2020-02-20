using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArch.Utilities.Core.Service;
using CleanArch.Utilities.GenericCrud.Repository;
using CleanArch.Utilities.GenericCrud.Services.Create;
using Moq;
using NUnit.Framework;
using Test.CleanArch.Utilities.GenericCrud._TestArtifacts;

namespace Test.CleanArch.Utilities.GenericCrud.Services.Create
{
    public class TestCreateHandler
    {
        private readonly IMapper _mapper;

        public TestCreateHandler()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperConfiguration>());
            _mapper = new Mapper(mapperConfiguration);
        }

        [Test]
        public async Task Handle_Ok()
        {
            var guid = Guid.NewGuid();
            var request = new MockCreateRequest { Value = "test" };
            var repository = new Mock<IGenericRepository<MockEntity, Guid>>();
            repository.Setup(x => x.CreateAsync(It.IsAny<MockEntity>()))
                .ReturnsAsync((MockEntity entity) =>
                {
                    entity.Id = guid;
                    return entity;
                });

            var handler = new CreateHandler<MockCreateRequest, MockEntity, Guid>(repository.Object, _mapper);


            var result = await handler.Handle(request, CancellationToken.None);


            Assert.AreEqual(ServiceResponseStatus.Ok, result.Status);
            Assert.IsNull(result.ValidationErrors);
            Assert.IsNull(result.Errors);
            Assert.IsNull(result.Exception);
            Assert.AreEqual(request.Value, result.Payload.Value);
            Assert.AreEqual(guid, result.Payload.Id);

            repository.Verify(x => x.CreateAsync(It.IsAny<MockEntity>()), Times.Once);
        }
    }
}
