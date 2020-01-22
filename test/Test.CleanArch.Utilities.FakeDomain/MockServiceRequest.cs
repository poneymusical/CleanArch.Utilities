using CleanArch.Utilities.Mediatr.Service;

namespace Test.CleanArch.Utilities.FakeDomain
{
    public class MockServiceRequest : IServiceRequest<MockServiceResponse>
    {
        public int Value { get; set; }
    }
}
