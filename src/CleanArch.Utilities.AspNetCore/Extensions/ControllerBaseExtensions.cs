using CleanArch.Utilities.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Utilities.AspNetCore.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static IActionResult FromServiceResponseStatus<T>(this ControllerBase controllerBase, ServiceResponse<T> serviceResponse)
            => controllerBase.StatusCode((int) serviceResponse.Status.ToHttpStatusCode(), serviceResponse);
    } 
}