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
    public abstract class BaseValidationPipelineBehavior<TRequest>
    {
        protected readonly IEnumerable<IValidator<TRequest>> Validators;

        protected BaseValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            Validators = validators;
        }

        protected List<ValidationFailure> GetValidationFailures(TRequest request)
        {
            var validationContext = new ValidationContext<TRequest>(request);

            return Validators
                .Select(validator => validator.Validate(validationContext))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure != null)
                .ToList();
        }
    }

    public class ValidationPipelineBehavior<TRequest> : BaseValidationPipelineBehavior<TRequest>,
        IServicePipelineBehavior<TRequest>
        where TRequest : IServiceRequest
    {
        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
            : base(validators)
        {
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
    }

    public class ValidationPipelineBehavior<TRequest, TResponse> : BaseValidationPipelineBehavior<TRequest>, 
        IServicePipelineBehavior<TRequest, TResponse>
        where TRequest : IServiceRequest<TResponse>
    {
        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators) 
            : base(validators) { }
        
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
    }
}