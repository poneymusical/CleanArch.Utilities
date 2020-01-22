using System;
using System.Net;
using CleanArch.Utilities.Core.Service;

namespace CleanArch.Utilities.AspNetCore.Extensions
{
    public static class ServiceResponseStatusExtensions
    {
        public static HttpStatusCode ToHttpStatusCode(this ServiceResponseStatus serviceResponseStatus)
        {
            switch (serviceResponseStatus)
            {
                case ServiceResponseStatus.Ok:
                    return System.Net.HttpStatusCode.OK;
                case ServiceResponseStatus.NotFound:
                    return System.Net.HttpStatusCode.NotFound;
                case ServiceResponseStatus.BadRequest:
                    return System.Net.HttpStatusCode.BadRequest;
                case ServiceResponseStatus.Conflict:
                    return System.Net.HttpStatusCode.Conflict;
                case ServiceResponseStatus.UnknownError:
                    return System.Net.HttpStatusCode.InternalServerError;
                case ServiceResponseStatus.Forbidden:
                    return System.Net.HttpStatusCode.Forbidden;
                default:
                    throw new ArgumentOutOfRangeException(nameof(serviceResponseStatus), serviceResponseStatus, null);
            }
        }  
    }
}