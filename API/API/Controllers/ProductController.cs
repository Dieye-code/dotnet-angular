using API.Application.Features.Products.Commands;
using API.Application.Features.Products.Queries;
using API.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ApiControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await Mediator.Send(new GetProductQuery());
            return Ok(products);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new FindProductQuery() { Id = id});
            if (result.Value.GetType() == typeof(Error) && (Error)result.Value == Errors.General.NotFound(null, null))
            {
                return NotFound(((Error)result.Value).Message);
            }
            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid categoryId, string libelle, string description, int price, double quantity, IFormFile file)
        {
            var result = await Mediator.Send(new CreateProductCommand() { CategoryId = categoryId, Libelle = libelle, Description = description, Price = price, Quantity = quantity, File = file });
            if (result.Value.GetType() == typeof(Error))
            {
                    return NotFound(result.Value);
            }
            return Ok(result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.Value.GetType() == typeof(Error))
            {
                return NotFound(result.Value);
            }
            return Ok(result.Value);
        }

    }
}
