using MediatR;

namespace CleanArch.Utilities.Core.Service
{
    public interface IServiceRequest<TResponse> : IRequest<ServiceResponse<TResponse>>
    {
    }

    public interface IServiceRequest : IRequest<ServiceResponse>
    {
    }
}
