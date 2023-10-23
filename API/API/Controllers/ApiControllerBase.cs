using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ApiControllerBase : ControllerBase
{

    private IMediator? _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

}
