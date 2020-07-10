using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArch.Utilities.Core.PipelineBehavior;
using CleanArch.Utilities.Core.Service;
using FluentValidation;
using MediatR;

namespace CleanArch.Utilities.Core.Validation
{
    public class ValidationPipelineBehavior<TRequest,TResponse> : IServicePipelineBehavior<TRequest, TResponse>
        where TRequest : IServiceRequest<TResponse>
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
                return ServiceResponse<TResponse>.BadRequest(failures);

            return await next();
        }
    }
}