using System;
using System.Linq;
using System.Reflection;
using CleanArch.Utilities.Core.PipelineBehavior;
using CleanArch.Utilities.Core.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Utilities.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, Assembly serviceAssembly) =>
            services.AddServices(serviceAssembly, 
                typeof(ValidationPipelineBehavior<>), 
                typeof(ValidationPipelineBehavior<,>));

        public static IServiceCollection AddServices(this IServiceCollection services, Assembly serviceAssembly,
            params Type[] pipelineBehaviorTypes)
        {
            services.AddValidatorsFromAssembly(serviceAssembly);
            services.AddMediatR(serviceAssembly);
            foreach (var pipelineBehaviorType in pipelineBehaviorTypes)
                services.AddServicePipelineBehavior(serviceAssembly, pipelineBehaviorType);

            return services;
        }

        public static IServiceCollection AddServicePipelineBehavior<TPipelineBehavior>(this IServiceCollection services,
            Assembly assemblyContainingRequests) =>
            services.AddServicePipelineBehavior(assemblyContainingRequests, typeof(TPipelineBehavior));
        

        public static IServiceCollection AddServicePipelineBehavior(this IServiceCollection services,
            Assembly assemblyContainingRequests, Type servicePipelineBehavior)
        {
            if (servicePipelineBehavior.ImplementsInterface(typeof(IServicePipelineBehavior<,>)))
                if (servicePipelineBehavior.IsGenericType)
                    services.AddGenericBehaviorForAllGenericRequestsInAssembly(assemblyContainingRequests, servicePipelineBehavior);
                else
                    services.AddClosedBehaviorForGenericRequestsThatItImplements(servicePipelineBehavior);

            if (servicePipelineBehavior.ImplementsInterface(typeof(IServicePipelineBehavior<>)))
                if (servicePipelineBehavior.IsGenericType)
                    services.AddGenericBehaviorForAllClosedRequestsInAssembly(assemblyContainingRequests, servicePipelineBehavior);
                else
                    services.AddClosedBehaviorForClosedRequestsThatItImplements(servicePipelineBehavior);
            
            return services;
        }

        private static bool ImplementsInterface(this Type type, Type interfaceType)
        {
            return type.GetInterfaces()
                .Select(i => interfaceType.IsGenericType ? i.GetGenericTypeDefinition() : i)
                .Any(currentInterface => currentInterface.GetGenericTypeDefinition().Name == interfaceType.Name);
        }
    }
}