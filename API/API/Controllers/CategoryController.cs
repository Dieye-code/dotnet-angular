using API.Application.Features.Categories.Commands;
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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Find(Guid id)
    {
        var category = await Mediator.Send(new FindCategoryQuery() { Id = id});
        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] CreateCategoryCommand command)
    {
        var category = await Mediator.Send(command);
        return Ok(category);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand command)
    {
        var category = await Mediator.Send(command);
        return Ok(category);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var categoryId = await Mediator.Send(new DeleteCategoryCommand() { Id = id});
        return Ok(categoryId);
    }

}
