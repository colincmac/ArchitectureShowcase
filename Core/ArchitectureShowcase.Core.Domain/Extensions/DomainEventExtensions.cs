using ArchitectureShowcase.Core.Domain.Contracts;
using Azure.Messaging;
using System.Reflection;

namespace ArchitectureShowcase.Core.Domain.Extensions;

public static class DomainEventExtensions
{
    private static Dictionary<string, Type> NameToType => _nameToType.Value;

    private static readonly Lazy<Dictionary<string, Type>> _nameToType = new(() => EventTypes.ToDictionary(t =>
    {
        VersionedEventAttribute attr = t.GetCustomAttributes(false)
        .OfType<VersionedEventAttribute>()
        .FirstOrDefault() ?? new VersionedEventAttribute(t.Namespace ?? "ArchitectureShowcase", t.Name);

        return attr.FullVersionedName;
    }));

    private static readonly Lazy<IEnumerable<Type>> _eventTypes = new(() => AppDomain.CurrentDomain.GetAssemblies()
        .Where(x => !x.IsDynamic && x.GetName().Name?.StartsWith("Microsoft") != true && x.GetName().Name?.StartsWith("System") != true)
        .SelectMany(x => x.DefinedTypes.Where(IsDomainEventType)));

    private static IEnumerable<Type> EventTypes => _eventTypes.Value;

    private static bool IsDomainEventType(Type type) => !type.IsGenericTypeDefinition && typeof(IDomainEvent).IsAssignableFrom(type);

    private static Type GetEventDataType(string eventTypeName)
    {
        return NameToType.TryGetValue(eventTypeName, out Type? eventType) ? eventType : throw new InvalidOperationException($"Could not find type for event {eventTypeName}.");
    }

    public static VersionedEventAttribute GetEventMetadata(this IDomainEvent eventItem)
    {
        return eventItem.GetType()
            .GetCustomAttributes(false)
            .OfType<VersionedEventAttribute>()
            .First();
    }

    public static CloudEvent? ToCloudEvent<TEvent>(this TEvent eventItem, string eventSource, string eventSubject) where TEvent : class, IDomainEvent
    {
        VersionedEventAttribute metadata = eventItem.GetEventMetadata();

        return new CloudEvent(eventSource, metadata.FullVersionedName, eventItem)
        {
            Subject = eventSubject,
        };
    }

    public static IDomainEvent ToDomainEvent(this CloudEvent eventItem)
    {
        if (eventItem.Data is null) throw new InvalidOperationException($"Cloud Event's Data property is null. Cannot convert to domain event.");
        var eventName = eventItem.Type;

        Type eventType = GetEventDataType(eventName);

        MethodInfo? method = typeof(DomainEventExtensions).GetMethod(nameof(FromBinaryData), BindingFlags.Public | BindingFlags.Static)?
            .MakeGenericMethod(eventType);
        if (method == null) throw new InvalidOperationException($"Could not find Generic method {nameof(FromBinaryData)}");
        return (IDomainEvent)method.Invoke(null, new[] { eventItem.Data });
    }


    public static TDomainEvent FromBinaryData<TDomainEvent>(this BinaryData eventData) where TDomainEvent : class, IDomainEvent
    {
        var obj = eventData.ToObjectFromJson<TDomainEvent>();
        if (obj != null && obj is TDomainEvent domainEvent)
        {
            return domainEvent;
        }
        throw new InvalidCastException($"Could not cast event data to domain event type {typeof(TDomainEvent).Name}.");
    }
}