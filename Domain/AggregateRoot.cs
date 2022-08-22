using ArchitectureShowcase.Domain.Contracts;

namespace ArchitectureShowcase.Domain;

// Combination of multiple event sourcing models, including
// https://github.com/charlessolar/Aggregates.NET
// https://github.com/dotnet-architecture/eShopOnContainers/blob/dev/src/Services/Ordering/Ordering.Domain/SeedWork/Entity.cs
public class AggregateRoot : Entity
{
    public int Version { get; }

    protected AggregateRoot()
    {
    }

    private List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public AggregateRoot(IEnumerable<IDomainEvent> eventItems)
    {
        if (eventItems == null) return;

        foreach (var domainEvent in eventItems)
        {
            Mutate(domainEvent);
            Version++;
        }
    }

    public void AddDomainEvent(IDomainEvent eventItem) => _domainEvents.Add(eventItem);

    public void RemoveDomainEvent(IDomainEvent eventItem) => _domainEvents.Remove(eventItem);

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void Apply(IEnumerable<IDomainEvent> eventItems)
    {
        foreach (var eventItem in eventItems)
        {
            Apply(eventItem);
        }
    }

    protected void Apply(IDomainEvent eventItem)
    {
        Mutate(eventItem);
        AddDomainEvent(eventItem);
    }

    private void Mutate(IDomainEvent eventItem) =>
        ((dynamic)this).On((dynamic)eventItem);
}