using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArch.Utilities.Core.PipelineBehavior;
using CleanArch.Utilities.Core.Service;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Utilities.Core.Validation
{
    public class ValidationPipelineBehavior<TRequest, TResponse> 
        : IServicePipelineBehavior<TRequest, TResponse>, IServicePipelineBehavior<TRequest>
        where TRequest : IServiceRequest<TResponse>, IServiceRequest
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        async Task<ServiceResponse<TResponse>> IPipelineBehavior<TRequest, ServiceResponse<TResponse>>.Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<ServiceResponse<TResponse>> next)
        {
            var failures = GetValidationFailures(request);
            if (failures.Any())
                return ServiceResponseFactory.BadRequest<TResponse>(failures);

            return await next();
        }

        async Task<ServiceResponse> IPipelineBehavior<TRequest, ServiceResponse>.Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<ServiceResponse> next)
        {
            var failures = GetValidationFailures(request);
            if (failures.Any())
                return ServiceResponseFactory.BadRequest(failures);

            return await next();
        }

        private List<ValidationFailure> GetValidationFailures(TRequest request)
        {
            var validationContext = new ValidationContext(request);

            return _validators
                .Select(validator => validator.Validate(validationContext))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure != null)
                .ToList();
        }
    }
}