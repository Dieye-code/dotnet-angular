using API.Application.Features.Categories.Commands;
using API.Application.Features.Categories.Queries;
using API.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
        var result = await Mediator.Send(new FindCategoryQuery() { Id = id});

        if(result.GetType() == typeof(Error))
        {
            return NotFound(result.Value);
        }

        return Ok(result.Value);
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
        var result = await Mediator.Send(command);
        if(result.Value.GetType() == typeof(Error))
        {
            return BadRequest(result.Value);
        }
        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeleteCategoryCommand() { Id = id });
        if (result.Value.GetType() == typeof(Error))
        {
            return BadRequest(result.Value);
        }
        return NoContent();
    }

}
