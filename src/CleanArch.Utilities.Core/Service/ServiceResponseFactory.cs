using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace CleanArch.Utilities.Core.Service
{
    public static class ServiceResponseFactory
    {
        public static ServiceResponse Ok() =>
            new(ServiceResponseStatus.Ok);

        public static ServiceResponse NotFound() =>
            new(ServiceResponseStatus.NotFound);

        public static ServiceResponse BadRequest(IEnumerable<ValidationFailure> validationErrors) =>
            new(ServiceResponseStatus.BadRequest) { ValidationErrors = validationErrors };

        public static ServiceResponse BadRequest(params ValidationFailure[] validationErrors) =>
            new(ServiceResponseStatus.BadRequest) { ValidationErrors = validationErrors };

        public static ServiceResponse BadRequest(IEnumerable<string> errors) =>
            new(ServiceResponseStatus.BadRequest) { Errors = errors };
        
        public static ServiceResponse BadRequest(params string[] errors) =>
            new(ServiceResponseStatus.BadRequest) { Errors = errors };

        public static ServiceResponse Conflict() =>
            new(ServiceResponseStatus.Conflict);

        public static ServiceResponse Forbidden() =>
            new(ServiceResponseStatus.Forbidden);

        public static ServiceResponse Unauthorized() =>
            new(ServiceResponseStatus.Unauthorized);
        
        public static ServiceResponse UnknownError(Exception exception) =>
            new(exception);
        
                
        public static ServiceResponse<T> Ok<T>(T payload) =>
            new(payload, ServiceResponseStatus.Ok);
        
        public static ServiceResponse<T> NotFound<T>() =>
            new(default, ServiceResponseStatus.NotFound);

        public static ServiceResponse<T> BadRequest<T>(IEnumerable<ValidationFailure> validationErrors) =>
            new(default, ServiceResponseStatus.BadRequest) { ValidationErrors = validationErrors };
        
        public static ServiceResponse<T> BadRequest<T>(params ValidationFailure[] validationErrors) =>
            new(default, ServiceResponseStatus.BadRequest) { ValidationErrors = validationErrors };

        public static ServiceResponse<T> BadRequest<T>(IEnumerable<string> errors) =>
            new(default, ServiceResponseStatus.BadRequest) { Errors = errors };
        
        public static ServiceResponse<T> BadRequest<T>(params string[] errors) =>
            new(default, ServiceResponseStatus.BadRequest) { Errors = errors };

        public static ServiceResponse<T> Conflict<T>() =>
            new(default, ServiceResponseStatus.Conflict);

        public static ServiceResponse<T> Conflict<T>(T conflicting) =>
            new(conflicting, ServiceResponseStatus.Conflict);
        
        public static ServiceResponse<T> Forbidden<T>() =>
            new(default, ServiceResponseStatus.Forbidden);

        public static ServiceResponse<T> Unauthorized<T>() =>
            new(default, ServiceResponseStatus.Unauthorized);
        
        public static ServiceResponse<T> UnknownError<T>(Exception exception) =>
            new(exception);
    }
}