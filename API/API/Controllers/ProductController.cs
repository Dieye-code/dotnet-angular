using API.Application.Features.Products.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ApiControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Create(Guid categoryId, string libelle, string description, int price, double quantity, IFormFile file)
        {
            var product = await Mediator.Send(new CreateProductCommand() { CategoryId = categoryId, Libelle = libelle, Description = description, Price = price, Quantity = quantity, File = file });
            return Ok(product);
        }

    }
}
