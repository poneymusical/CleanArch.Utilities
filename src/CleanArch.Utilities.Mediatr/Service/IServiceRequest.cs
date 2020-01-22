using MediatR;

namespace CleanArch.Utilities.Mediatr.Service
{
    public interface IServiceRequest<T> : IRequest<ServiceResponse<T>>
    {
    }
}
