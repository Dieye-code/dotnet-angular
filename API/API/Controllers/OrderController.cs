using API.Application.Features.Orders.Command;
using API.Application.Features.Orders.Queries;
using API.Domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrderController : ApiControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = await Mediator.Send(new GetOrderQuey());
            return Ok(orders);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Find(Guid id)
        {
            var result = await Mediator.Send(new FindOrderQuery() { Id = id });
            if(result.GetType() == typeof(Error))
            {
                return NotFound(result.Value);
            }
            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {

            var result = await Mediator.Send(command);

            if(result.IsFailure)
            {
                return BadRequest(result.Value);
            }

            return Ok(result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateOrderCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.IsFailure)
            {
                if(result.Value.GetType() == typeof(Error))
                    return NotFound(result.Value);
                else
                    return BadRequest(result.Value);
            }
            return Ok(result.Value);
        }

    }
}
