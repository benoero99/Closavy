namespace Closavy.Server.Exceptions;

public class EntityNotFoundException : CustomExceptionBase
{
    protected override string DefaultTitle => "Entity not found!";

    public EntityNotFoundException(string? message) : base(message)
    {
    }

    public EntityNotFoundException(string? message, string? title) : base(message, title)
    {
    }

    public EntityNotFoundException(string? message, Exception? innerException, string? title) : base(message, innerException, title)
    {
    }

}