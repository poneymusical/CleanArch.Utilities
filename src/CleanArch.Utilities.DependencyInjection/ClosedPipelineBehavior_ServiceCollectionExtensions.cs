using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CleanArch.Utilities.Core.PipelineBehavior;
using CleanArch.Utilities.Core.Service;
using MediatR;
using MediatR.Registration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Utilities.DependencyInjection
{
    internal static class ClosedPipelineBehaviorServiceCollectionExtensions
    {
        internal static void AddGenericBehaviorForAllClosedRequestsInAssembly(this IServiceCollection services,
            Assembly assemblyContainingRequests, Type servicePipelineBehavior)
        {
            var requests = assemblyContainingRequests.GetTypes().Where(type => type.GetInterface(nameof(IServiceRequest)) != null).ToList();
            foreach (var request in requests) 
                services.BindPipelineBehaviorToClosedRequest(servicePipelineBehavior, request);
        }
        
        internal static void AddClosedBehaviorForClosedRequestsThatItImplements(this IServiceCollection services, Type servicePipelineBehavior)
        {
            var requests = GetImplementedClosedRequests(servicePipelineBehavior);
            foreach (var request in requests)
                services.BindPipelineBehaviorToClosedRequest(servicePipelineBehavior, request); 
        }

        private static IEnumerable<Type> GetImplementedClosedRequests(Type servicePipelineBehavior) =>
            servicePipelineBehavior.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition().Name == typeof(IServicePipelineBehavior<>).Name)
                .Select(i => i.GenericTypeArguments[0])
                .ToList();

        private static void BindPipelineBehaviorToClosedRequest(this IServiceCollection services, Type servicePipelineBehavior, Type request)
        {
            var iPipelineBehavior = typeof(IPipelineBehavior<,>).MakeGenericType(request, typeof(ServiceResponse));
            var matchingPipelineBehavior = servicePipelineBehavior.IsOpenGeneric()
                ? servicePipelineBehavior.MakeGenericType(request)
                : servicePipelineBehavior;
            services.AddTransient(iPipelineBehavior, matchingPipelineBehavior);
        }
    }
}