using System.Threading;
using System.Threading.Tasks;
using CleanArch.Utilities.Core.PipelineBehavior;
using CleanArch.Utilities.Core.Service;
using MediatR;
using Test.CleanArch.Utilities.DependencyInjection._TestUtils.Requests;

namespace Test.CleanArch.Utilities.DependencyInjection._TestUtils.PipelineBehaviors
{
    public class MockGenericPipelineBehavior : IServicePipelineBehavior<MockOpenServiceRequest, int>
    {
        public Task<ServiceResponse<int>> Handle(MockOpenServiceRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<ServiceResponse<int>> next)
        {
            throw new System.NotImplementedException();
        }
    }
}