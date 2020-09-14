using System;
using System.Linq;
using System.Reflection;
using CleanArch.Utilities.Core.PipelineBehavior;
using CleanArch.Utilities.Core.Service;
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
        
        public static IServiceCollection AddServicePipelineBehavior(this IServiceCollection services,
            Assembly assemblyContainingRequests, Type servicePipelineBehavior)
        {
            if (servicePipelineBehavior.GetInterface(typeof(IServicePipelineBehavior<,>).Name) != null)
                services.AddBehaviorForGenericIServiceRequest(assemblyContainingRequests, servicePipelineBehavior);

            if (servicePipelineBehavior.GetInterface(typeof(IServicePipelineBehavior<>).Name) != null)
                services.AddBehaviorForClosedIServiceRequest(assemblyContainingRequests, servicePipelineBehavior);

            return services;
        }

        public static IServiceCollection AddBehaviorForGenericIServiceRequest(
            this IServiceCollection services,
            Assembly assemblyContainingRequests,
            Type servicePipelineBehavior)
        {
            var iServicePipelineBehavior = typeof(IServicePipelineBehavior<,>);
            if (servicePipelineBehavior.GetInterface(iServicePipelineBehavior.Name) == null)
                throw new ArgumentException(
                    $"{servicePipelineBehavior.FullName} does not implement {iServicePipelineBehavior.FullName}");

            var iServiceRequest = typeof(IServiceRequest<>);
            var requests = assemblyContainingRequests.GetTypes()
                .Where(type => type.GetInterface(iServiceRequest.Name) != null).ToList();

            foreach (var request in requests)
            {
                var response = request.GetInterface(iServiceRequest.Name).GenericTypeArguments[0];
                var serviceResponse = typeof(ServiceResponse<>).MakeGenericType(response);
                var iPipelineBehavior = typeof(IPipelineBehavior<,>).MakeGenericType(request, serviceResponse);
                var matchingPipelineBehavior = servicePipelineBehavior.MakeGenericType(request, response);
                services.AddTransient(iPipelineBehavior, matchingPipelineBehavior);
            }

            return services;
        }

        private static IServiceCollection AddBehaviorForClosedIServiceRequest(this IServiceCollection services,
            Assembly assemblyContainingRequests, Type servicePipelineBehavior)
        {
            //Check if servicePipelineBehaviorImplementation implements IServicePipelineBehavior
            var iServicePipelineBehavior = typeof(IServicePipelineBehavior<>);
            if (servicePipelineBehavior.GetInterface(iServicePipelineBehavior.Name) == null)
                throw new ArgumentException(
                    $"{servicePipelineBehavior.FullName} does not implement {iServicePipelineBehavior.FullName}");

            var iServiceRequest = typeof(IServiceRequest);
            var requests = assemblyContainingRequests.GetTypes()
                .Where(type => type.GetInterface(iServiceRequest.Name) != null).ToList();

            var serviceResponse = typeof(ServiceResponse<>);

            foreach (var request in requests)
            {
                var iPipelineBehavior = typeof(IPipelineBehavior<,>).MakeGenericType(request, serviceResponse);
                var matchingPipelineBehavior = servicePipelineBehavior.MakeGenericType(request, serviceResponse);
                services.AddTransient(iPipelineBehavior, matchingPipelineBehavior);
            }

            return services;
        }
    }
}