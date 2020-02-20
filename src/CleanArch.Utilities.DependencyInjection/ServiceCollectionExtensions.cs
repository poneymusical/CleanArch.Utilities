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
        public static IServiceCollection AddServicePipelineBehavior(this IServiceCollection services,
            Assembly assemblyContainingRequests, Type servicePipelineBehavior)
        {
            //Check if servicePipelineBehaviorImplementation implements IServicePipelineBehavior
            var iServicePipelineBehavior = typeof(IServicePipelineBehavior<,>);
            if (servicePipelineBehavior.GetInterface(iServicePipelineBehavior.Name) == null)
                throw new ArgumentException($"{servicePipelineBehavior.FullName} does not implement {iServicePipelineBehavior.FullName}");

            //In given assembly, get all types that implement IServiceRequest
            var iServiceRequest = typeof(IServiceRequest<>);
            var requests = assemblyContainingRequests.GetTypes()
                .Where(type => type.GetInterface(iServiceRequest.Name) != null).ToList();
            
            foreach (var request in requests)
            {
                //requestType implements IServiceRequest<TResponse> => get TResponse
                var response = request.GetInterface(iServiceRequest.Name).GenericTypeArguments[0];
                //Create ServiceResponse<response>
                var serviceResponse = typeof(ServiceResponse<>).MakeGenericType(response);
                //Create IPipelineBehavior<requestType,ServiceResponse<response>>
                var iPipelineBehavior = typeof(IPipelineBehavior<,>).MakeGenericType(request, serviceResponse);
                //Create ServicePipelineBehavior<requestType, responseType> => equivalent to PipelineBehavior<request, ServiceResponse<response>> 
                var matchingPipelineBehavior = servicePipelineBehavior.MakeGenericType(request, response);
                //Add to services
                //Equivalent to AddTransient<IPipelineBehavior<Request,ServiceResponse<Response>>, ServicePipelineBehavior<Request, Response>>
                services.AddTransient(iPipelineBehavior, matchingPipelineBehavior); 
            }
            
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, Assembly serviceAssembly)
        {
            services.AddValidatorsFromAssembly(serviceAssembly);
            services.AddMediatR(serviceAssembly);
            services.AddServicePipelineBehavior(serviceAssembly, typeof(ValidationPipelineBehavior<,>));

            return services;
        }

    }
}