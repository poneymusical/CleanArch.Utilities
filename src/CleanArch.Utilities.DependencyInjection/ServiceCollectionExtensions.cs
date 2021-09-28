using System;
using System.Linq;
using System.Reflection;
using CleanArch.Utilities.Core.PipelineBehavior;
using CleanArch.Utilities.Core.Service;
using CleanArch.Utilities.Core.Validation;
using FluentValidation;
using MediatR;
using MediatR.Registration;
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
                services.AddBehaviorForGenericIServiceRequest(assemblyContainingRequests, servicePipelineBehavior);

            if (servicePipelineBehavior.ImplementsInterface(typeof(IServicePipelineBehavior<>)))
                if (servicePipelineBehavior.IsOpenGeneric())
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
        
        private static void AddBehaviorForGenericIServiceRequest(this IServiceCollection services,
            Assembly assemblyContainingRequests, Type servicePipelineBehavior)
        {
            var iServiceRequest = typeof(IServiceRequest<>);
            var requests = assemblyContainingRequests.GetTypes()
                .Where(type => type.GetInterface(iServiceRequest.Name) != null).ToList();
            foreach (var request in requests) 
                services.BindPipelineBehaviorToGenericRequest(servicePipelineBehavior, request);
        }

        private static void BindPipelineBehaviorToGenericRequest(this IServiceCollection services, Type servicePipelineBehavior, Type request)
        {
            var response = request.GetInterface(typeof(IServiceRequest<>).Name)!.GenericTypeArguments[0];
            var serviceResponse = typeof(ServiceResponse<>).MakeGenericType(response);
            var iPipelineBehavior = typeof(IPipelineBehavior<,>).MakeGenericType(request, serviceResponse);
            var matchingPipelineBehavior = servicePipelineBehavior.IsOpenGeneric()
                ? servicePipelineBehavior.MakeGenericType(request, response)
                : servicePipelineBehavior;
            services.AddTransient(iPipelineBehavior, matchingPipelineBehavior);
        }
    }
}