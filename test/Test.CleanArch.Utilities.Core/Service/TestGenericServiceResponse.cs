using System;
using AutoFixture;
using CleanArch.Utilities.Core.Service;
using FluentAssertions;
using Xunit;

namespace Test.CleanArch.Utilities.Core.Service
{
    public class TestGenericServiceResponse
    {
        [Fact]
        public void TestDefaultCtor()
        {
            var serviceResponse = new ServiceResponse<object>();
            serviceResponse.Status.Should().Be(ServiceResponseStatus.Ok);
            serviceResponse.ValidationErrors.Should().BeEmpty();
            serviceResponse.Errors.Should().BeEmpty();
            serviceResponse.Exception.Should().BeNull();
            serviceResponse.Payload.Should().Be(default);
        }
        
        [Fact]
        public void TestCtorWithStatus()
        {
            var fixture = new Fixture();
            var serviceResponseStatus = fixture.Create<ServiceResponseStatus>();
            var payload = fixture.Create<object>();
            var serviceResponse = new ServiceResponse<object>(payload, serviceResponseStatus);
            serviceResponse.Status.Should().Be(serviceResponseStatus);
            serviceResponse.ValidationErrors.Should().BeEmpty();
            serviceResponse.Errors.Should().BeEmpty();
            serviceResponse.Exception.Should().BeNull();
            serviceResponse.Payload.Should().Be(payload);
        }

        [Fact]
        public void TestCtorWithException()
        {
            var fixture = new Fixture();
            var exception = fixture.Create<Exception>();
            var serviceResponse = new ServiceResponse<object>(exception);
            serviceResponse.Status.Should().Be(ServiceResponseStatus.UnknownError);
            serviceResponse.ValidationErrors.Should().BeEmpty();
            serviceResponse.Errors.Should().BeEmpty();
            serviceResponse.Exception.Should().Be(exception);
            serviceResponse.Payload.Should().Be(default);
        }

        [Fact]
        public void TestConversionServiceResponseToPayload()
        {
            var fixture = new Fixture();
            var serviceResponseStatus = fixture.Create<ServiceResponseStatus>();
            var payload = fixture.Create<int>();
            var serviceResponse = new ServiceResponse<int>(payload, serviceResponseStatus);
            ((int)serviceResponse).Should().Be(payload);
        }

        [Fact]
        public void TestConversionPayloadToServiceResponse()
        {
            var fixture = new Fixture();
            var payload = fixture.Create<int>();
            ((ServiceResponse<int>)payload).Should()
                .BeEquivalentTo(new ServiceResponse<int>(payload, ServiceResponseStatus.Ok));
        }
    }
}