using System;
using System.Linq;
using System.Reflection;
using CleanArch.Utilities.Mediatr.PipelineBehavior;
using CleanArch.Utilities.Mediatr.Service;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Utilities.DependencyInjection.Mediatr
{
    public static class PipelineBehaviorInjectionExtensions
    {
        public static IServiceCollection AddServicePipelineBehavior(this IServiceCollection services,
            Assembly assemblyContainingRequests, Type servicePipelineBehaviorClassType)
        {
            //Check if pipelineBehaviorType implements IServicePipelineBehavior
            var iServicePipelineBehaviorType = typeof(IServicePipelineBehavior<,>); //IServicePipelineBehavior<,>

            var closedIServicePipelineBehaviorType = servicePipelineBehaviorClassType.GetInterface(iServicePipelineBehaviorType.Name); //IServicePipelineBehavior<,> (or closed if a closed type was given
            
            if (closedIServicePipelineBehaviorType == null)
                throw new ArgumentException($"{servicePipelineBehaviorClassType.FullName} does not implement {iServicePipelineBehaviorType.FullName}");

            //In given assembly, get all types that implement IServiceRequest
            var iServiceRequestType = typeof(IServiceRequest<>); //IServiceRequest<>
            
            var requestTypes = assemblyContainingRequests.GetTypes()
                .Where(type => type.GetInterface(iServiceRequestType.Name) != null).ToList();
            
            foreach (var requestType in requestTypes) //MockServiceRequest
            {
                var responseType = requestType.GetInterface(iServiceRequestType.Name).GenericTypeArguments[0]; //MockResponse
                
                var serviceResponseType = typeof(ServiceResponse<>).MakeGenericType(responseType); //ServiceResponse<MockResponse>
                
                var iPipelineBehaviorClosedType = typeof(IPipelineBehavior<,>).MakeGenericType(requestType, serviceResponseType);
                
                services.AddTransient(iPipelineBehaviorClosedType, servicePipelineBehaviorClassType.MakeGenericType(requestType, responseType));
            }


            return services;
        }
    }
}