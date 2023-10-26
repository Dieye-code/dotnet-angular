using API.Application.Features.Products.Commands;
using API.Application.Features.Products.Queries;
using Microsoft.AspNetCore.Http;
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
            var product = Mediator.Send(new FindProductQuery() { Id = id});
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid categoryId, string libelle, string description, int price, double quantity, IFormFile file)
        {
            var product = await Mediator.Send(new CreateProductCommand() { CategoryId = categoryId, Libelle = libelle, Description = description, Price = price, Quantity = quantity, File = file });
            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
        {
            var product = await Mediator.Send(command);
            return Ok(product);
        }

    }
}
