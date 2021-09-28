using System;
using AutoFixture;
using CleanArch.Utilities.Core.Service;
using FluentAssertions;
using Xunit;

namespace Test.CleanArch.Utilities.Core.Service
{
    public class TestClosedServiceResponse
    {
        [Fact]
        public void TestDefaultCtor()
        {
            var serviceResponse = new ServiceResponse();
            serviceResponse.Status.Should().Be(ServiceResponseStatus.Ok);
            serviceResponse.ValidationErrors.Should().BeEmpty();
            serviceResponse.Errors.Should().BeEmpty();
            serviceResponse.Exception.Should().BeNull();
        }

        [Fact]
        public void TestCtorWithStatus()
        {
            var fixture = new Fixture();
            var serviceResponseStatus = fixture.Create<ServiceResponseStatus>();
            var serviceResponse = new ServiceResponse(serviceResponseStatus);
            serviceResponse.Status.Should().Be(serviceResponseStatus);
            serviceResponse.ValidationErrors.Should().BeEmpty();
            serviceResponse.Errors.Should().BeEmpty();
            serviceResponse.Exception.Should().BeNull();
        }

        [Fact]
        public void TestCtorWithException()
        {
            var fixture = new Fixture();
            var exception = fixture.Create<Exception>();
            var serviceResponse = new ServiceResponse(exception);
            serviceResponse.Status.Should().Be(ServiceResponseStatus.UnknownError);
            serviceResponse.ValidationErrors.Should().BeEmpty();
            serviceResponse.Errors.Should().BeEmpty();
            serviceResponse.Exception.Should().Be(exception);
        }
    }
}