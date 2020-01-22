using System.Threading;
using System.Threading.Tasks;
using CleanArch.Utilities.Mediatr.Service;

namespace Test.CleanArch.Utilities.FakeDomain
{
    public class MockServiceHandler : IServiceRequestHandler<MockServiceRequest, MockServiceResponse>
    {
        public async Task<ServiceResponse<MockServiceResponse>> Handle(MockServiceRequest request, CancellationToken cancellationToken)
        {
            return new ServiceResponse<MockServiceResponse>
            {
                Status = ServiceResponseStatus.Ok,
                Payload = new MockServiceResponse
                {
                    Result = request.Value * -1
                }
            };
        }
    }
}