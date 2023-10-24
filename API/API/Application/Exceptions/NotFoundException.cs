using System.Net;

namespace API.Application.Exceptions;

public class NotFoundException : CustomException
{
    public NotFoundException(string message) : base(message, null, HttpStatusCode.NotFound)
    {
    }
}
