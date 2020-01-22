using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using CleanArch.Utilities.Core.Service;
using CleanArch.Utilities.GenericCrud.Entities;
using CleanArch.Utilities.GenericCrud.Services.Create;
using CleanArch.Utilities.GenericCrud.Services.ReadSingle;
using CleanArch.Utilities.GenericCrud.Services.Update;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Utilities.GenericCrud.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGenericCrud(this IServiceCollection services, Assembly assembly)
        {
            services.AddAutoMapper(assembly);

            var entities = assembly.GetTypes()
                .Where(type => type.GetInterface(typeof(IIdentifiable<>).Name) != null).ToList();

            foreach (var entity in entities)
            {
                var id = entity.GetInterface(typeof(IIdentifiable<>).Name).GenericTypeArguments[0];

                AddGenericCreate(services, assembly, entity, id);
                AddGenericUpdate(services, assembly, entity, id);
                AddGenericReadSingle(services, assembly, entity, id);
            }

            return services;
        }

        private static void AddGenericCreate(IServiceCollection services, Assembly assembly, Type entity, Type id)
        {
            var serviceResponseBound = typeof(ServiceResponse<>).MakeGenericType(entity);
            var iCreateRequestBound = typeof(ICreateRequest<,>).MakeGenericType(entity, id);

            var createRequests = assembly.GetTypes()
                .Where(type => type.GetInterface(iCreateRequestBound.Name) != null).ToList();

            foreach (var createRequest in createRequests)
            {
                var requestHandlerInterfaceBound = typeof(IRequestHandler<,>).MakeGenericType(createRequest, serviceResponseBound);
                var createHandlerBound = typeof(CreateHandler<,,>).MakeGenericType(createRequest, entity, id);
                services.AddTransient(requestHandlerInterfaceBound, createHandlerBound);
            }
        }

        private static void AddGenericUpdate(IServiceCollection services, Assembly assembly, Type entity, Type id)
        {
            var serviceResponseBound = typeof(ServiceResponse<>).MakeGenericType(entity);
            var iUpdateRequestBound = typeof(IUpdateRequest<,>).MakeGenericType(entity, id);

            var updateRequests = assembly.GetTypes()
                .Where(type => type.GetInterface(iUpdateRequestBound.Name) != null).ToList();

            foreach (var updateRequest in updateRequests)
            {
                var requestHandlerInterfaceBound = typeof(IRequestHandler<,>).MakeGenericType(updateRequest, serviceResponseBound);
                var updateHandlerBound = typeof(UpdateHandler<,,>).MakeGenericType(updateRequest, entity, id);
                services.AddTransient(requestHandlerInterfaceBound, updateHandlerBound);
            }
        }

        private static void AddGenericReadSingle(IServiceCollection services, Assembly assembly, Type entity, Type id)
        {
            var serviceResponseBound = typeof(ServiceResponse<>).MakeGenericType(entity);
            var iReadSingleRequestBound = typeof(IReadSingleRequest<,>).MakeGenericType(entity, id);

            var readSingleRequests = assembly.GetTypes()
                .Where(type => type.GetInterface(iReadSingleRequestBound.Name) != null).ToList();

            foreach (var readSingleRequest in readSingleRequests)
            {
                var requestHandlerInterfaceBound = typeof(IRequestHandler<,>).MakeGenericType(readSingleRequest, serviceResponseBound);
                var readSingleHandlerBound = typeof(ReadSingleHandler<,,>).MakeGenericType(readSingleRequest, entity, id);
                services.AddTransient(requestHandlerInterfaceBound, readSingleHandlerBound);
            }
        }
    }
}