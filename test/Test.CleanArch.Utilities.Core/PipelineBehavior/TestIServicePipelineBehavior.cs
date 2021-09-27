using CleanArch.Utilities.Core.PipelineBehavior;
using CleanArch.Utilities.Core.Service;
using FluentAssertions;
using MediatR;
using Moq;
using Test.CleanArch.Utilities.Core._TestUtils;
using Xunit;

namespace Test.CleanArch.Utilities.Core.PipelineBehavior
{
    public class TestIServicePipelineBehavior
    {
        [Fact]
        public void TestClosedIServicePipelineBehavior()
        {
            var mockPipelineBehavior = new Mock<IServicePipelineBehavior<MockRequest>>().Object;
            mockPipelineBehavior.GetType().Should().Implement<IPipelineBehavior<MockRequest, ServiceResponse>>();
        }

        [Fact]
        public void TestGenericIServicePipelineBehavior()
        {
            var mockPipelineBehavior = new Mock<IServicePipelineBehavior<MockRequest, object>>().Object;
            mockPipelineBehavior.GetType().Should().Implement<IPipelineBehavior<MockRequest, ServiceResponse<object>>>();
        }
    }
}