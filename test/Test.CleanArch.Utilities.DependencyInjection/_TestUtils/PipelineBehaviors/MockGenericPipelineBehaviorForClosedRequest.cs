using System.Threading;
using System.Threading.Tasks;
using CleanArch.Utilities.Core.PipelineBehavior;
using CleanArch.Utilities.Core.Service;
using MediatR;

namespace Test.CleanArch.Utilities.DependencyInjection._TestUtils.PipelineBehaviors
{
    public class MockGenericPipelineBehaviorForClosedRequest<TRequest> : IServicePipelineBehavior<TRequest> where TRequest : IServiceRequest
    {
        public Task<ServiceResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<ServiceResponse> next)
        {
            throw new System.NotImplementedException();
        }
    }
}