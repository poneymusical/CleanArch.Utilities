﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CleanArch.Utilities.Core.Service;
using CleanArch.Utilities.Core.Validation;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;
using Test.CleanArch.Utilities.Core._TestUtils;
using Xunit;

namespace Test.CleanArch.Utilities.Core.Validation
{
    public class TestClosedValidationPipelineBehavior
    {
        [Fact]
        public async Task Handle_ValidationReturnsFailure()
        {
            var fixture = new Fixture();
            var failures = fixture.CreateMany<ValidationFailure>().ToList();
            var request = fixture.Create<MockRequest>();
            var cancellationToken = fixture.Create<CancellationToken>();
            var requestHandlerDelegate = fixture.Create<RequestHandlerDelegate<ServiceResponse>>();

            var validator = new Mock<IValidator<MockRequest>>();
            validator.Setup(x => x.Validate(It.Is<ValidationContext<MockRequest>>(ctx => ctx.InstanceToValidate == request)))
                .Returns(new ValidationResult(failures));

            IPipelineBehavior<MockRequest, ServiceResponse> behavior = new ValidationPipelineBehavior<MockRequest>(new[] { validator.Object });
            var result = await behavior.Handle(request, cancellationToken, requestHandlerDelegate);
            result.Status.Should().Be(ServiceResponseStatus.BadRequest);
            result.ValidationErrors.Should().BeEquivalentTo(failures);
        }
        
        [Fact]
        public async Task Handle_ValidationReturnsNoFailure()
        {
            var fixture = new Fixture();
            var request = fixture.Create<MockRequest>();
            var cancellationToken = fixture.Create<CancellationToken>();
            var serviceResponse = fixture.Create<ServiceResponse>();
            
            var validator = new Mock<IValidator<MockRequest>>();
            validator.Setup(x => x.Validate(It.Is<ValidationContext<MockRequest>>(ctx => ctx.InstanceToValidate == request)))
                .Returns(new ValidationResult());
            var requestHandlerDelegate = new RequestHandlerDelegate<ServiceResponse>(() => Task.FromResult(serviceResponse));

            IPipelineBehavior<MockRequest, ServiceResponse> behavior = new ValidationPipelineBehavior<MockRequest>(new[] { validator.Object });
            var result = await behavior.Handle(request, cancellationToken, requestHandlerDelegate);
            result.Should().Be(serviceResponse);
        }
        
        [Fact]
        public async Task Handle_NoValidators()
        {
            var fixture = new Fixture();
            var request = fixture.Create<MockRequest>();
            var cancellationToken = fixture.Create<CancellationToken>();
            var serviceResponse = fixture.Create<ServiceResponse>();
            
            var requestHandlerDelegate = new RequestHandlerDelegate<ServiceResponse>(() => Task.FromResult(serviceResponse));

            IPipelineBehavior<MockRequest, ServiceResponse> behavior = new ValidationPipelineBehavior<MockRequest>(System.Array.Empty<IValidator<MockRequest>>());
            var result = await behavior.Handle(request, cancellationToken, requestHandlerDelegate);
            result.Should().Be(serviceResponse);
        }
    }
}