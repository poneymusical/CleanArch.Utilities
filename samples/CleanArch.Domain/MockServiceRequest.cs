using CleanArch.Utilities.Core.Service;

namespace CleanArch.Domain
{
    public class MockServiceRequest : IServiceRequest<MockServiceResponse>
    {
        public int Value { get; set; }
    }
}
