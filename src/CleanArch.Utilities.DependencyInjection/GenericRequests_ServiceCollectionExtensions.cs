using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CleanArch.Utilities.Core.PipelineBehavior;
using CleanArch.Utilities.Core.Service;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Utilities.DependencyInjection
{
    internal static class GenericRequestsServiceCollectionExtensions
    {
        internal static void AddGenericBehaviorForAllGenericRequestsInAssembly(this IServiceCollection services,
            Assembly assemblyContainingRequests, Type servicePipelineBehavior)
        {
            var requestResponseCombinations = GetAllGenericRequestsFromAssembly(assemblyContainingRequests);
            foreach (var (request, response) in requestResponseCombinations) 
                services.BindPipelineBehaviorToGenericRequest(servicePipelineBehavior, request, response);
        }

        internal static void AddClosedBehaviorForGenericRequestsThatItImplements(this IServiceCollection services,
            Type servicePipelineBehavior)
        {
            var requestResponseCombinations = GetImplementedGenericRequests(servicePipelineBehavior);
            foreach (var (request, response) in requestResponseCombinations)
                services.BindPipelineBehaviorToGenericRequest(servicePipelineBehavior, request, response);
        }
        
        private static IEnumerable<(Type, Type)> GetImplementedGenericRequests(Type servicePipelineBehavior) =>
            servicePipelineBehavior.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition().Name == typeof(IServicePipelineBehavior<,>).Name)
                .Select(i => (i.GenericTypeArguments[0], i.GenericTypeArguments[1]))
                .ToList();

        private static IEnumerable<(Type, Type)> GetAllGenericRequestsFromAssembly(Assembly assembly)
        {
            var requests = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition().Name == typeof(IServiceRequest<>).Name));
            
            var result = new List<(Type, Type)>();
            foreach (var request in requests)
            {
                result.AddRange(request.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition().Name == typeof(IServiceRequest<>).Name)
                    .Select(i => (request, i.GenericTypeArguments[0])));
            }

            return result;
        }

        private static void BindPipelineBehaviorToGenericRequest(this IServiceCollection services, 
            Type servicePipelineBehavior, Type request, Type response)
        {
            var serviceResponse = typeof(ServiceResponse<>).MakeGenericType(response);
            var iPipelineBehavior = typeof(IPipelineBehavior<,>).MakeGenericType(request, serviceResponse);
            var matchingPipelineBehavior = servicePipelineBehavior.IsGenericType
                ? servicePipelineBehavior.MakeGenericType(request, response)
                : servicePipelineBehavior;
            services.AddTransient(iPipelineBehavior, matchingPipelineBehavior);
        }
    }
}