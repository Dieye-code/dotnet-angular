using System.Net;

namespace API.Application.Exceptions;

public class ForbiddenAccessException : CustomException
{

    public ForbiddenAccessException(string message = null) : base(message, null, HttpStatusCode.Forbidden) { }

}
