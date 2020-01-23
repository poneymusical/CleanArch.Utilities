using System.Threading.Tasks;
using CleanArch.Domain;
using CleanArch.Utilities.AspNetCore.Extensions;
using CleanArch.Utilities.GenericCrud.Services.ReadPaginated;
using CleanArch.Utilities.GenericCrud.Services.ReadSingle;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var request = new ReadSingleRequest<MyEntity, int> { Id = id };
            var response = await _mediator.Send(request);
            return this.FromServiceResponseStatus(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var request = new ReadPaginatedRequest<MyEntity, int> { PageIndex = pageIndex, PageSize = pageSize };
            var response = await _mediator.Send(request);
            return this.FromServiceResponseStatus(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MyEntityCreateRequest request)
        {
            var response = await _mediator.Send(request);
            return this.FromServiceResponseStatus(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MyEntityUpdateRequest request)
        {
            var response = await _mediator.Send(request);
            return this.FromServiceResponseStatus(response);
        }
    }
}