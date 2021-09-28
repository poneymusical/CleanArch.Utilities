using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using CleanArch.Utilities.Core.Service;
using FluentAssertions;
using FluentValidation.Results;
using Xunit;

namespace Test.CleanArch.Utilities.Core.Service
{
    public class TestServiceResponseFactory
    {
        [Fact]
        public void TestClosedFactories()
        {
            var fixture = new Fixture();
            var iEnumerableValidationFailures = fixture.CreateMany<ValidationFailure>().ToList();
            var arrayValidationFailures = fixture.CreateMany<ValidationFailure>().ToArray();
            var iEnumerableErrors = fixture.CreateMany<string>().ToList();
            var arrayErrors = fixture.CreateMany<string>().ToArray();
            var exception = fixture.Create<Exception>();
            
            ServiceResponseFactory.Ok().Check(ServiceResponseStatus.Ok, Array.Empty<ValidationFailure>(), Array.Empty<string>(), null);
            ServiceResponseFactory.NotFound().Check(ServiceResponseStatus.NotFound, Array.Empty<ValidationFailure>(), Array.Empty<string>(), null);
            ServiceResponseFactory.BadRequest(iEnumerableValidationFailures).Check(ServiceResponseStatus.BadRequest, iEnumerableValidationFailures, Array.Empty<string>(), null);
            ServiceResponseFactory.BadRequest(arrayValidationFailures).Check(ServiceResponseStatus.BadRequest, arrayValidationFailures, Array.Empty<string>(), null);
            ServiceResponseFactory.BadRequest(iEnumerableErrors).Check(ServiceResponseStatus.BadRequest, Array.Empty<ValidationFailure>(), iEnumerableErrors, null);
            ServiceResponseFactory.BadRequest(arrayErrors).Check(ServiceResponseStatus.BadRequest, Array.Empty<ValidationFailure>(), arrayErrors, null);
            ServiceResponseFactory.Conflict().Check(ServiceResponseStatus.Conflict, Array.Empty<ValidationFailure>(), Array.Empty<string>(), null);
            ServiceResponseFactory.Forbidden().Check(ServiceResponseStatus.Forbidden, Array.Empty<ValidationFailure>(), Array.Empty<string>(), null);
            ServiceResponseFactory.Unauthorized().Check(ServiceResponseStatus.Unauthorized, Array.Empty<ValidationFailure>(), Array.Empty<string>(), null);
            ServiceResponseFactory.UnknownError(exception).Check(ServiceResponseStatus.UnknownError, Array.Empty<ValidationFailure>(), Array.Empty<string>(), exception);
        }

        [Fact]
        public void TestGenericFactories()
        {
            var fixture = new Fixture();
            var iEnumerableValidationFailures = fixture.CreateMany<ValidationFailure>().ToList();
            var arrayValidationFailures = fixture.CreateMany<ValidationFailure>().ToArray();
            var iEnumerableErrors = fixture.CreateMany<string>().ToList();
            var arrayErrors = fixture.CreateMany<string>().ToArray();
            var exception = fixture.Create<Exception>();
            var payload = fixture.Create<int>();
            
            ServiceResponseFactory.Ok(payload).Check(ServiceResponseStatus.Ok, payload, Array.Empty<ValidationFailure>(), Array.Empty<string>(), null);
            ServiceResponseFactory.NotFound<int>().Check(ServiceResponseStatus.NotFound, default, Array.Empty<ValidationFailure>(), Array.Empty<string>(), null);
            ServiceResponseFactory.BadRequest<int>(iEnumerableValidationFailures).Check(ServiceResponseStatus.BadRequest, default, iEnumerableValidationFailures, Array.Empty<string>(), null);
            ServiceResponseFactory.BadRequest<int>(arrayValidationFailures).Check(ServiceResponseStatus.BadRequest, default, arrayValidationFailures, Array.Empty<string>(), null);
            ServiceResponseFactory.BadRequest<int>(iEnumerableErrors).Check(ServiceResponseStatus.BadRequest, default, Array.Empty<ValidationFailure>(), iEnumerableErrors, null);
            ServiceResponseFactory.BadRequest<int>(arrayErrors).Check(ServiceResponseStatus.BadRequest, default, Array.Empty<ValidationFailure>(), arrayErrors, null);
            ServiceResponseFactory.Conflict<int>().Check(ServiceResponseStatus.Conflict, default, Array.Empty<ValidationFailure>(), Array.Empty<string>(), null);
            ServiceResponseFactory.Conflict(payload).Check(ServiceResponseStatus.Conflict, payload, Array.Empty<ValidationFailure>(), Array.Empty<string>(), null);
            ServiceResponseFactory.Forbidden<int>().Check(ServiceResponseStatus.Forbidden, default, Array.Empty<ValidationFailure>(), Array.Empty<string>(), null);
            ServiceResponseFactory.Unauthorized<int>().Check(ServiceResponseStatus.Unauthorized, default, Array.Empty<ValidationFailure>(), Array.Empty<string>(), null);
            ServiceResponseFactory.UnknownError<int>(exception).Check(ServiceResponseStatus.UnknownError, default, Array.Empty<ValidationFailure>(), Array.Empty<string>(), exception);
        }
    }

    internal static class ServiceResponseExtensions
    {
        internal static void Check(this ServiceResponse serviceResponse, ServiceResponseStatus status,
            IEnumerable<ValidationFailure> validationFailures, IEnumerable<string> errors, Exception exception)
        {
            serviceResponse.Status.Should().Be(status);
            serviceResponse.ValidationErrors.Should().BeEquivalentTo(validationFailures);
            serviceResponse.Errors.Should().BeEquivalentTo(errors);
            serviceResponse.Exception.Should().Be(exception);
        }

        internal static void Check<T>(this ServiceResponse<T> serviceResponse, ServiceResponseStatus status,
            T payload, IEnumerable<ValidationFailure> validationFailures, IEnumerable<string> errors,
            Exception exception)
        {
            serviceResponse.Should().BeOfType<ServiceResponse<int>>();
            serviceResponse.Check(status, validationFailures, errors, exception);
            serviceResponse.Payload.Should().Be(payload);
        }
    } 
}