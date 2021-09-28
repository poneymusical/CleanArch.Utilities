using CleanArch.Utilities.Core.Service;
using FluentAssertions;
using MediatR;
using Moq;
using Test.CleanArch.Utilities.Core._TestUtils;
using Xunit;

namespace Test.CleanArch.Utilities.Core.Service
{
    public class TestIServiceRequestHandler
    {
        [Fact]
        public void TestClosedIServiceRequestHandler()
        {
            var mockServiceHandler = new Mock<IServiceRequestHandler<MockRequest>>().Object;
            mockServiceHandler.GetType().Should().Implement<IRequestHandler<MockRequest, ServiceResponse>>();
        }

        [Fact]
        public void TestGenericIServiceRequestHandler()
        {
            var mockServiceHandler = new Mock<IServiceRequestHandler<MockRequest, object>>().Object;
            mockServiceHandler.GetType().Should().Implement<IRequestHandler<MockRequest, ServiceResponse<object>>>();
        }
    }
}