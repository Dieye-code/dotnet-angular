using API.Application.Features.Orders.Command;
using API.Application.Features.Orders.Queries;
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

    }
}
