using API.Application.Features.Orders.Command;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrderController : ApiControllerBase
    {

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
