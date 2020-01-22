using CleanArch.Utilities.Core.Service;
using MediatR;

namespace CleanArch.Utilities.Core.PipelineBehavior
{
    public interface IServicePipelineBehavior<in TRequest, TResponse> : IPipelineBehavior<TRequest, ServiceResponse<TResponse>>
        where TRequest : IServiceRequest<TResponse>
    {
    }
}