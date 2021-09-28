using System.Threading;
using System.Threading.Tasks;
using CleanArch.Utilities.Core.PipelineBehavior;
using CleanArch.Utilities.Core.Service;
using MediatR;
using Test.CleanArch.Utilities.DependencyInjection._TestUtils.Requests;

namespace Test.CleanArch.Utilities.DependencyInjection._TestUtils.PipelineBehaviors
{
    public class MockClosedPipelineBehaviorForGenericRequest 
        : IServicePipelineBehavior<MockGenericServiceRequest, int>, IServicePipelineBehavior<AnotherMockGenericServiceRequest, long>
    {
        public Task<ServiceResponse<int>> Handle(MockGenericServiceRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<ServiceResponse<int>> next)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<long>> Handle(AnotherMockGenericServiceRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<ServiceResponse<long>> next)
        {
            throw new System.NotImplementedException();
        }
    }
}