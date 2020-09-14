using MediatR;

namespace CleanArch.Utilities.Core.Service
{
    public interface IServiceRequestHandler<in TRequest, TResponse> : IRequestHandler<TRequest, ServiceResponse<TResponse>>
        where TRequest : IServiceRequest<TResponse>
    {
    }

    public interface IServiceRequestHandler<in TRequest> : IRequestHandler<TRequest, ServiceResponse>
        where TRequest : IServiceRequest
    {
    }
}
