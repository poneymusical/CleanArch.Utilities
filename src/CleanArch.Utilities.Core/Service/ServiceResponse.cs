using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace CleanArch.Utilities.Core.Service
{
    public class ServiceResponse<T> : ServiceResponse
    {
        public T Payload { get; set; }
        
        public ServiceResponse()
        {
        }

        public ServiceResponse(T payload, ServiceResponseStatus status)
        {
            Payload = payload;
            Status = status;
        }

        public ServiceResponse(Exception exception)
        {
            Exception = exception;
            Status = ServiceResponseStatus.UnknownError;
        }
    }

    public class ServiceResponse
    {
        public ServiceResponseStatus Status { get; set; }
        public IEnumerable<ValidationFailure> ValidationErrors { get; set; } = new List<ValidationFailure>();
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public Exception Exception { get; set; }

        public ServiceResponse()
        {
        }

        public ServiceResponse(ServiceResponseStatus status)
        {
            Status = status;
        }

        public ServiceResponse(Exception exception)
        {
            Exception = exception;
            Status = ServiceResponseStatus.UnknownError;
        }
    }
}