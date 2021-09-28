using System.Threading;
using System.Threading.Tasks;
using CleanArch.Utilities.Core.PipelineBehavior;
using CleanArch.Utilities.Core.Service;
using MediatR;
using Test.CleanArch.Utilities.DependencyInjection._TestUtils.Requests;

namespace Test.CleanArch.Utilities.DependencyInjection._TestUtils.PipelineBehaviors
{
    public class MockClosedPipelineBehaviorForClosedRequest : IServicePipelineBehavior<MockClosedServiceRequest>, IServicePipelineBehavior<AnotherMockClosedServiceRequest>
    {
        public Task<ServiceResponse> Handle(MockClosedServiceRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<ServiceResponse> next)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse> Handle(AnotherMockClosedServiceRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<ServiceResponse> next)
        {
            throw new System.NotImplementedException();
        }
    }
}