using MediatR;

namespace Mediatr.Utilities.Service
{
    public interface IServiceRequest<T> : IRequest<ServiceResponse<T>>
    {
    }
}
