using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace CleanArch.Utilities.Core.Service
{
    public static class ServiceResponseFactory
    {
        public static ServiceResponse Ok() =>
            new ServiceResponse(ServiceResponseStatus.Ok);

        public static ServiceResponse NotFound() =>
            new ServiceResponse(ServiceResponseStatus.NotFound);

        public static ServiceResponse BadRequest(IEnumerable<ValidationFailure> validationErrors) =>
            new ServiceResponse(ServiceResponseStatus.BadRequest) { ValidationErrors = validationErrors };

        public static ServiceResponse BadRequest(params ValidationFailure[] validationErrors) =>
            new ServiceResponse(ServiceResponseStatus.BadRequest) { ValidationErrors = validationErrors };

        public static ServiceResponse BadRequest(IEnumerable<string> errors) =>
            new ServiceResponse(ServiceResponseStatus.BadRequest) { Errors = errors };
        
        public static ServiceResponse BadRequest(params string[] errors) =>
            new ServiceResponse(ServiceResponseStatus.BadRequest) { Errors = errors };

        public static ServiceResponse Conflict() =>
            new ServiceResponse(ServiceResponseStatus.Conflict);

        public static ServiceResponse Forbidden() =>
            new ServiceResponse(ServiceResponseStatus.Forbidden);

        public static ServiceResponse Unauthorized() =>
            new ServiceResponse(ServiceResponseStatus.Unauthorized);
        
        public static ServiceResponse UnknownError(Exception exception) =>
            new ServiceResponse(exception);
        
                
        public static ServiceResponse<T> Ok<T>(T payload) =>
            new ServiceResponse<T>(payload, ServiceResponseStatus.Ok);
        
        public static ServiceResponse<T> NotFound<T>() =>
            new ServiceResponse<T>(default, ServiceResponseStatus.NotFound);

        public static ServiceResponse<T> BadRequest<T>(IEnumerable<ValidationFailure> validationErrors) =>
            new ServiceResponse<T>(default, ServiceResponseStatus.BadRequest) { ValidationErrors = validationErrors };
        
        public static ServiceResponse<T> BadRequest<T>(params ValidationFailure[] validationErrors) =>
            new ServiceResponse<T>(default, ServiceResponseStatus.BadRequest) { ValidationErrors = validationErrors };

        public static ServiceResponse<T> BadRequest<T>(IEnumerable<string> errors) =>
            new ServiceResponse<T>(default, ServiceResponseStatus.BadRequest) { Errors = errors };
        
        public static ServiceResponse<T> BadRequest<T>(params string[] errors) =>
            new ServiceResponse<T>(default, ServiceResponseStatus.BadRequest) { Errors = errors };

        public static ServiceResponse<T> Conflict<T>() =>
            new ServiceResponse<T>(default, ServiceResponseStatus.Conflict);

        public static ServiceResponse<T> Conflict<T>(T conflicting) =>
            new ServiceResponse<T>(conflicting, ServiceResponseStatus.Conflict);
        
        public static ServiceResponse<T> Forbidden<T>() =>
            new ServiceResponse<T>(default, ServiceResponseStatus.Forbidden);

        public static ServiceResponse<T> Unauthorized<T>() =>
            new ServiceResponse<T>(default, ServiceResponseStatus.Unauthorized);
        
        public static ServiceResponse<T> UnknownError<T>(Exception exception) =>
            new ServiceResponse<T>(exception);
    }
}