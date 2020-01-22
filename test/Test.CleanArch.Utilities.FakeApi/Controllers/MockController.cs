using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Test.CleanArch.Utilities.FakeDomain;

namespace Test.CleanArch.Utilities.FakeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{value}")]
        public async Task<IActionResult> Get(int value)
        {
            var request = new MockServiceRequest { Value = value };
            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}