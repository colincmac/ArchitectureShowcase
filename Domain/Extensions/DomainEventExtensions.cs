using ArchitectureShowcase.Domain.Contracts;

namespace ArchitectureShowcase.Domain.Extensions;

public static class DomainEventExtensions
{
    public static VersionedEventAttribute? GetEventMetadata(this IDomainEvent eventItem)
    {
        return eventItem.GetType()
            .GetCustomAttributes(false)
            .OfType<VersionedEventAttribute>()
            .FirstOrDefault();
    }
}