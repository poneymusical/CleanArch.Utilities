using CleanArch.Utilities.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Utilities.AspNetCore.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static IActionResult FromServiceResponseStatus(this ControllerBase controllerBase, ServiceResponse serviceResponse)
            => controllerBase.StatusCode((int) serviceResponse.Status.ToHttpStatusCode(), serviceResponse);
    } 
}