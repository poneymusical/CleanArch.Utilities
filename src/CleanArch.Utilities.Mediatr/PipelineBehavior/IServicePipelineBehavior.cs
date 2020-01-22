using CleanArch.Utilities.Mediatr.Service;
using MediatR;

namespace CleanArch.Utilities.Mediatr.PipelineBehavior
{
    public interface IServicePipelineBehavior<in TRequest, TResponse> : IPipelineBehavior<TRequest, ServiceResponse<TResponse>>
        where TRequest : IServiceRequest<TResponse>
    {
    }
}