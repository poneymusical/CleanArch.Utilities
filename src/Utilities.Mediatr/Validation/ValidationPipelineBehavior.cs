using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Mediatr.Utilities.Service;
using MediatR;

namespace Mediatr.Utilities.Validation
{
    public class ValidationPipelineBehavior<TRequest,TResponse> : IPipelineBehavior<TRequest, ServiceResponse<TResponse>>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        async Task<ServiceResponse<TResponse>> IPipelineBehavior<TRequest, ServiceResponse<TResponse>>.Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<ServiceResponse<TResponse>> next)
        {
            var validationContext = new ValidationContext(request);

            var failures = _validators
                .Select(validator => validator.Validate(validationContext))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure != null)
                .ToList();

            if (failures.Any())
                return new ServiceResponse<TResponse>
                {
                    Status = ServiceResponseStatus.BadRequest,
                    ValidationErrors = failures,
                    Payload = default
                };

            return await next();
        }
    }
}