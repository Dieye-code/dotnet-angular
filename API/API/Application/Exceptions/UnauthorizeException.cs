namespace API.Application.Exceptions;

public class UnauthorizeException : CustomException
{
    public UnauthorizeException(string message = null) : base(message, null, System.Net.HttpStatusCode.Unauthorized)
    {

    }
}
