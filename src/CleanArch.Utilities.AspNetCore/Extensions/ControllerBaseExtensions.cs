using CleanArch.Utilities.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Utilities.AspNetCore.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static IActionResult FromServiceResponseStatus(this ControllerBase controllerBase,
            ServiceResponse serviceResponse, bool includeServiceResponseInBody = true)
            => includeServiceResponseInBody 
                ? controllerBase.StatusCode((int) serviceResponse.Status.ToHttpStatusCode(), serviceResponse)
                : (IActionResult) controllerBase.StatusCode((int) serviceResponse.Status.ToHttpStatusCode());

        public static IActionResult FromServiceResponseStatus<T>(this ControllerBase controllerBase,
            ServiceResponse<T> serviceResponse, bool includePayloadOnly = false)
            => controllerBase.StatusCode((int) serviceResponse.Status.ToHttpStatusCode(),
                includePayloadOnly ? serviceResponse.Payload : (object) serviceResponse);
    }
}