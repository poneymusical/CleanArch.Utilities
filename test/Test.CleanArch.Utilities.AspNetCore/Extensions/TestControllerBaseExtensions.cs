using AutoFixture;
using CleanArch.Utilities.AspNetCore.Extensions;
using CleanArch.Utilities.Core.Service;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Test.CleanArch.Utilities.AspNetCore.Extensions
{
    public class TestControllerBaseExtensions
    {
        [Fact]
        public void TestFromServiceResponseClosed_IncludeServiceResponseInBodyTrue()
        {
            var fixture = new Fixture();

            var controller = new MockController();
            var serviceResponse = fixture.Create<ServiceResponse>();

            var response = controller.FromServiceResponse(serviceResponse);
            response.Should().BeOfType<ObjectResult>();
            var cast = (ObjectResult)response;
            cast.StatusCode.Should().Be(serviceResponse.Status.ToHttpStatusCode());
            cast.Value.Should().Be(serviceResponse);
        }
        
        [Fact]
        public void TestFromServiceResponseClosed_IncludeServiceResponseInBodyFalse()
        {
            var fixture = new Fixture();

            var controller = new MockController();
            var serviceResponse = fixture.Create<ServiceResponse>();

            var response = controller.FromServiceResponse(serviceResponse, false);
            response.Should().BeOfType<StatusCodeResult>();
            var cast = (StatusCodeResult)response;
            cast.StatusCode.Should().Be(serviceResponse.Status.ToHttpStatusCode());
        }
        
        [Fact]
        public void TestFromServiceResponseGeneric_IncludePayloadOnlyTrue()
        {
            var fixture = new Fixture();

            var controller = new MockController();
            var serviceResponse = fixture.Create<ServiceResponse<int>>();

            var response = controller.FromServiceResponse(serviceResponse, true);
            response.Should().BeOfType<ObjectResult>();
            var cast = (ObjectResult)response;
            cast.StatusCode.Should().Be(serviceResponse.Status.ToHttpStatusCode());
            cast.Value.Should().Be(serviceResponse.Payload);
        }
        
        [Fact]
        public void TestFromServiceResponseGeneric_IncludePayloadOnlyFalse()
        {
            var fixture = new Fixture();

            var controller = new MockController();
            var serviceResponse = fixture.Create<ServiceResponse<int>>();

            var response = controller.FromServiceResponse(serviceResponse);
            response.Should().BeOfType<ObjectResult>();
            var cast = (ObjectResult)response;
            cast.StatusCode.Should().Be(serviceResponse.Status.ToHttpStatusCode());
            cast.Value.Should().Be(serviceResponse);
        }

        private class MockController : ControllerBase
        {
        }
    }
}