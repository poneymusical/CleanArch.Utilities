using System.Threading.Tasks;
using CleanArch.Domain;
using CleanArch.Domain.GenericCrud.MyEntity;
using CleanArch.Utilities.AspNetCore.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Controllers
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
            return this.FromServiceResponseStatus(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MyEntityCreateRequest request)
        {
            var response = await _mediator.Send(request);
            return this.FromServiceResponseStatus(response);
        }
    }
}