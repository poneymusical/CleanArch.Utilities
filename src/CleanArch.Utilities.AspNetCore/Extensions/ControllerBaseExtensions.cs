using CleanArch.Utilities.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Utilities.AspNetCore.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static IActionResult FromServiceResponse(this ControllerBase controllerBase,
            ServiceResponse serviceResponse, bool includeServiceResponseInBody = true)
            => includeServiceResponseInBody 
                ? controllerBase.StatusCode(serviceResponse.Status.ToHttpStatusCode(), serviceResponse)
                : controllerBase.StatusCode(serviceResponse.Status.ToHttpStatusCode());

        public static IActionResult FromServiceResponse<T>(this ControllerBase controllerBase,
            ServiceResponse<T> serviceResponse, bool includePayloadOnly = false)
            => controllerBase.StatusCode(serviceResponse.Status.ToHttpStatusCode(),
                includePayloadOnly ? serviceResponse.Payload : serviceResponse);
    }
}