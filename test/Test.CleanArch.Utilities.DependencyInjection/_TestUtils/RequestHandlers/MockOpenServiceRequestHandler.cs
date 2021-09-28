using System.Threading;
using System.Threading.Tasks;
using CleanArch.Utilities.Core.Service;
using Test.CleanArch.Utilities.DependencyInjection._TestUtils.Requests;

namespace Test.CleanArch.Utilities.DependencyInjection._TestUtils.RequestHandlers
{
    public class MockOpenServiceRequestHandler : IServiceRequestHandler<MockOpenServiceRequest, int>
    {
        public Task<ServiceResponse<int>> Handle(MockOpenServiceRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    
}