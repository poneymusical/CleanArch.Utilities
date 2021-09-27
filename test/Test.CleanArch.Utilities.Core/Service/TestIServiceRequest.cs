using CleanArch.Utilities.Core.Service;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace Test.CleanArch.Utilities.Core.Service
{
    public class TestIServiceRequest
    {
        [Fact]
        public void TestClosedIServiceRequest()
        {
            var mockRequest = new Mock<IServiceRequest>().Object;
            mockRequest.GetType().Should().Implement<IRequest<ServiceResponse>>();
        }

        [Fact]
        public void TestGenericIServiceRequest()
        {
            var mockRequest = new Mock<IServiceRequest<object>>().Object;
            mockRequest.GetType().Should().Implement<IRequest<ServiceResponse<object>>>();
        }
    }
}