namespace Closavy.Server.Exceptions;

public class NameAlreadyTakenException : CustomExceptionBase
{
    protected override string DefaultTitle => "This name is already taken!";

    public NameAlreadyTakenException(string? message) : base(message)
    {
    }

    public NameAlreadyTakenException(string? message, string? title = null) : base(message, title)
    {
    }

    public NameAlreadyTakenException(string? message, Exception? innerException, string? title = null) : base(message, innerException, title)
    {
    }
}