using CleanArch.Utilities.AspNetCore.Extensions;
using CleanArch.Utilities.Core.Service;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Test.CleanArch.Utilities.AspNetCore.Extensions
{
    public class TestServiceResponseStatusExtensions
    {
        [Theory]
        [InlineData(ServiceResponseStatus.Ok, StatusCodes.Status200OK)]
        [InlineData(ServiceResponseStatus.NotFound, StatusCodes.Status404NotFound)]
        [InlineData(ServiceResponseStatus.BadRequest, StatusCodes.Status400BadRequest)]
        [InlineData(ServiceResponseStatus.Conflict, StatusCodes.Status409Conflict)]
        [InlineData(ServiceResponseStatus.UnknownError, StatusCodes.Status500InternalServerError)]
        [InlineData(ServiceResponseStatus.Unauthorized, StatusCodes.Status401Unauthorized)]
        [InlineData(ServiceResponseStatus.Forbidden, StatusCodes.Status403Forbidden)]
        public void TestToHttpStatusCode(ServiceResponseStatus status, int httpStatusCode) => 
            status.ToHttpStatusCode().Should().Be(httpStatusCode);
    }
}