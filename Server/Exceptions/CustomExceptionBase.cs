namespace Closavy.Server.Exceptions;

public abstract class CustomExceptionBase : Exception
{
    protected abstract string DefaultTitle { get; }
    public string Title { get; }

    protected CustomExceptionBase(string? message, string? title = null) : base(message)
    {
        Title = title ?? DefaultTitle;
    }

    protected CustomExceptionBase(string? message, Exception? innerException, string? title = null) : base(message, innerException)
    {
        Title = title ?? DefaultTitle;
    }
}