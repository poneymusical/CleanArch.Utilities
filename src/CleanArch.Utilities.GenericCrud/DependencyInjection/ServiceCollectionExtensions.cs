using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using CleanArch.Utilities.Core.Service;
using CleanArch.Utilities.GenericCrud.Entities;
using CleanArch.Utilities.GenericCrud.Services.Create;
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
                AddGenericCreate(services, assembly, entity);

            return services;
        }

        private static void AddGenericCreate(IServiceCollection services, Assembly assembly, Type entity)
        {
            var id = entity.GetInterface(typeof(IIdentifiable<>).Name).GenericTypeArguments[0];
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
    }
}