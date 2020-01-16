using MediatR;

namespace Mediatr.Utilities.Service
{
    public interface IServiceRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, ServiceResponse<TResponse>>
        where TRequest : IServiceRequest<TResponse>
    {
    }
}
