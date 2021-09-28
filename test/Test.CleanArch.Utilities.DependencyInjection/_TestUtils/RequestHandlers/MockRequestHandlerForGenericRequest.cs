using System.Threading;
using System.Threading.Tasks;
using CleanArch.Utilities.Core.Service;
using Test.CleanArch.Utilities.DependencyInjection._TestUtils.Requests;

namespace Test.CleanArch.Utilities.DependencyInjection._TestUtils.RequestHandlers
{
    public class MockRequestHandlerForGenericRequest : IServiceRequestHandler<MockGenericServiceRequest, int>
    {
        public Task<ServiceResponse<int>> Handle(MockGenericServiceRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    
}