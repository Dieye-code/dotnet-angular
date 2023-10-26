using CSharpFunctionalExtensions;

namespace API.Domain.Common;

public class Error : ValueObject
{
    public string Code { get; private set; }
    public string Message { get; private set; }
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return this.Code;
    }
}

public static class Errors
{
    public static class General
    {
        public static Error NotFound(string name, object key)
        {
            return new Error("record.not.found", $"{name} ({key}) is not found");
        }
        public static Error InternalServer()
        {
            return new Error("record.internal.server", "Erreur au niveau du serveur");
        }
    }
}
