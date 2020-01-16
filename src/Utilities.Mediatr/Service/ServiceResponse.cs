using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace Mediatr.Utilities.Service
{
    public class ServiceResponse<T>
    {
        public ServiceResponseStatus Status { get; set; }
        public T Payload { get; set; }

        public IEnumerable<ValidationFailure> ValidationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public Exception Exception { get; set; }      
    }
}