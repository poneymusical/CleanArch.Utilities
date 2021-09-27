using System;
using CleanArch.Utilities.Core.Service;
using Microsoft.AspNetCore.Http;

namespace CleanArch.Utilities.AspNetCore.Extensions
{
    public static class ServiceResponseStatusExtensions
    {
        public static int ToHttpStatusCode(this ServiceResponseStatus serviceResponseStatus) =>
            serviceResponseStatus switch
            {
                ServiceResponseStatus.Ok => StatusCodes.Status200OK,
                ServiceResponseStatus.NotFound => StatusCodes.Status404NotFound,
                ServiceResponseStatus.BadRequest => StatusCodes.Status400BadRequest,
                ServiceResponseStatus.Conflict => StatusCodes.Status409Conflict,
                ServiceResponseStatus.UnknownError => StatusCodes.Status500InternalServerError,
                ServiceResponseStatus.Unauthorized => StatusCodes.Status401Unauthorized,
                ServiceResponseStatus.Forbidden => StatusCodes.Status403Forbidden,
                _ => throw new ArgumentOutOfRangeException(nameof(serviceResponseStatus), serviceResponseStatus, null)
            };
    }
}