using System;
using CleanArch.Utilities.Core.Service;
using Microsoft.AspNetCore.Http;

namespace CleanArch.Utilities.AspNetCore.Extensions
{
    public static class ServiceResponseStatusExtensions
    {
        public static int ToHttpStatusCode(this ServiceResponseStatus serviceResponseStatus)
        {
            switch (serviceResponseStatus)
            {
                case ServiceResponseStatus.Ok:
                    return StatusCodes.Status200OK;
                case ServiceResponseStatus.NotFound:
                    return StatusCodes.Status404NotFound;
                case ServiceResponseStatus.BadRequest:
                    return StatusCodes.Status400BadRequest;
                case ServiceResponseStatus.Conflict:
                    return StatusCodes.Status409Conflict;
                case ServiceResponseStatus.UnknownError:
                    return StatusCodes.Status500InternalServerError;
                case ServiceResponseStatus.Unauthorized:
                    return StatusCodes.Status401Unauthorized;
                case ServiceResponseStatus.Forbidden:
                    return StatusCodes.Status403Forbidden;
                default:
                    throw new ArgumentOutOfRangeException(nameof(serviceResponseStatus), serviceResponseStatus, null);
            }
        }  
    }
}