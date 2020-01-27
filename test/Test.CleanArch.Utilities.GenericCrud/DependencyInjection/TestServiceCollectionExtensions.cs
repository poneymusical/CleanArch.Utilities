using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CleanArch.Utilities.Core.Service;
using CleanArch.Utilities.GenericCrud.DependencyInjection;
using CleanArch.Utilities.GenericCrud.Services.Create;
using CleanArch.Utilities.GenericCrud.Services.Delete;
using CleanArch.Utilities.GenericCrud.Services.ReadPaginated;
using CleanArch.Utilities.GenericCrud.Services.ReadSingle;
using CleanArch.Utilities.GenericCrud.Services.Update;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Test.CleanArch.Utilities.GenericCrud._TestArtifacts;

namespace Test.CleanArch.Utilities.GenericCrud.DependencyInjection
{
    public class TestServiceCollectionExtensions
    {
        [Test]
        public void AddGenericCrud()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var services = new ServiceCollection();
            services.AddGenericCrud(assembly);

            var createHandlerServiceDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(IRequestHandler<MockCreateRequest, ServiceResponse<MockEntity>>));
            Assert.IsNotNull(createHandlerServiceDescriptor);
            Assert.AreEqual(typeof(CreateHandler<MockCreateRequest, MockEntity, Guid>), createHandlerServiceDescriptor.ImplementationType);

            var deleteHandlerServiceDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(IRequestHandler<DeleteRequest<MockEntity, Guid>, ServiceResponse<Guid>>));
            Assert.IsNotNull(deleteHandlerServiceDescriptor);
            Assert.AreEqual(typeof(DeleteHandler<DeleteRequest<MockEntity, Guid>, MockEntity, Guid>), deleteHandlerServiceDescriptor.ImplementationType);

            var readPaginatedServiceDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(IRequestHandler<MockReadPaginatedRequest, ServiceResponse<IEnumerable<MockEntity>>>));
            Assert.IsNotNull(readPaginatedServiceDescriptor);
            Assert.AreEqual(typeof(ReadPaginatedHandler<MockReadPaginatedRequest, MockEntity, Guid>), readPaginatedServiceDescriptor.ImplementationType);

            var readSingleServiceDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(IRequestHandler<ReadSingleRequest<MockEntity, Guid>, ServiceResponse<MockEntity>>));
            Assert.IsNotNull(readSingleServiceDescriptor);
            Assert.AreEqual(typeof(ReadSingleHandler<ReadSingleRequest<MockEntity, Guid>, MockEntity, Guid>), readSingleServiceDescriptor.ImplementationType);

            var updateServiceDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(IRequestHandler<MockUpdateRequest, ServiceResponse<MockEntity>>));
            Assert.IsNotNull(updateServiceDescriptor);
            Assert.AreEqual(typeof(UpdateHandler<MockUpdateRequest, MockEntity, Guid>), updateServiceDescriptor.ImplementationType);
        }
    }
}