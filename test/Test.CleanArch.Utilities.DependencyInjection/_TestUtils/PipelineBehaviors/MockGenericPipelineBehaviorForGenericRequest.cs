using System.Threading;
using System.Threading.Tasks;
using CleanArch.Utilities.Core.PipelineBehavior;
using CleanArch.Utilities.Core.Service;
using MediatR;

namespace Test.CleanArch.Utilities.DependencyInjection._TestUtils.PipelineBehaviors
{
    public class MockGenericPipelineBehaviorForGenericRequest<TRequest, TResponse> : IServicePipelineBehavior<TRequest, TResponse> 
        where TRequest : IServiceRequest<TResponse>
    {
        public Task<ServiceResponse<TResponse>> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<ServiceResponse<TResponse>> next)
        {
            throw new System.NotImplementedException();
        }
    }
}