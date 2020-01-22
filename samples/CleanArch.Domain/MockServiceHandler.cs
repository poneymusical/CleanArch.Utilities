using System.Threading;
using System.Threading.Tasks;
using CleanArch.Utilities.Core.Service;

namespace CleanArch.Domain
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
                    Result = await Task.Run(() => request.Value * -1, cancellationToken)
                }
            };
        }
    }
}