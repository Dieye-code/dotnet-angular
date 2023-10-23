using API.Application.Features.Categories.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("categories")]
[ApiController]
public class CategoryController : ApiControllerBase
{

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var categories = await Mediator.Send(new GetCategoryQuery());
        return Ok(categories);
    }
}
