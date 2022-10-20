namespace ArchitectureShowcase.Core.Domain.Exceptions;
public class DomainOperationException : Exception
{
    public DomainOperationException()
    {
    }

    public DomainOperationException(string? message) : base(message)
    {
    }

    public DomainOperationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
