using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace CleanArch.Utilities.Core.Service
{
    public class ServiceResponse<T>
    {
        public ServiceResponseStatus Status { get; set; }
        public T Payload { get; set; }

        public IEnumerable<ValidationFailure> ValidationErrors { get; set; } = new List<ValidationFailure>();
        public IEnumerable<string> Errors { get; set; } = new List<string>();

        public Exception Exception { get; set; }

        public ServiceResponse()
        {
        }

        private ServiceResponse(T payload, ServiceResponseStatus status)
        {
            Payload = payload;
            Status = status;
        }

        private ServiceResponse(Exception exception)
        {
            Exception = exception;
            Status = ServiceResponseStatus.UnknownError;
        }

        public static ServiceResponse<T> Ok(T payload) =>
            new ServiceResponse<T>(payload, ServiceResponseStatus.Ok);

        public static ServiceResponse<T> NotFound() =>
            new ServiceResponse<T>(default, ServiceResponseStatus.NotFound);

        public static ServiceResponse<T> BadRequest(IEnumerable<ValidationFailure> validationErrors) =>
            new ServiceResponse<T>(default, ServiceResponseStatus.BadRequest) { ValidationErrors = validationErrors };

        public static ServiceResponse<T> BadRequest(IEnumerable<string> errors) =>
            new ServiceResponse<T>(default, ServiceResponseStatus.BadRequest) { Errors = errors };

        public static ServiceResponse<T> Conflict() =>
            new ServiceResponse<T>(default, ServiceResponseStatus.Conflict);

        public static ServiceResponse<T> Forbidden() =>
            new ServiceResponse<T>(default, ServiceResponseStatus.Forbidden);

        public static ServiceResponse<T> UnknownError(Exception exception) =>
            new ServiceResponse<T>(exception);
    }
}