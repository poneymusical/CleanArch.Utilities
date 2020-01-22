using MediatR;

namespace CleanArch.Utilities.Mediatr.Service
{
    public interface IServiceRequestHandler<in TRequest, TResponse> : IRequestHandler<TRequest, ServiceResponse<TResponse>>
        where TRequest : IServiceRequest<TResponse>
    {
    }
}
