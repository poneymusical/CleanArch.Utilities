using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArch.Domain;
using CleanArch.Utilities.AspNetCore.Extensions;
using CleanArch.Utilities.Core.Service;
using CleanArch.Utilities.GenericCrud.Services.Delete;
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
        public async Task<IActionResult> Get([FromBody] MyEntityFilter filter = null, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var request = new MyEntityReadPaginatedRequest { PageIndex = pageIndex, PageSize = pageSize, Filter = filter };
            var response = await _mediator.Send<ServiceResponse<IEnumerable<MyEntity>>>(request);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteRequest<MyEntity, int> { Id = id };
            var response = await _mediator.Send(request);
            return this.FromServiceResponseStatus(response);
        }
    }
}