using ArchitectureShowcase.Domain.Contracts;

namespace ArchitectureShowcase.Domain;
public static class EventRegistry
{
    // Should probably use some kind of Code generator like here
    // https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/eventgrid/Azure.Messaging.EventGrid/EventGridSourceGenerator/src
    private static readonly Lazy<Dictionary<string, Type>> _nameToType = new(() => EventTypes.ToDictionary(t =>
    {
        var attr = t.GetCustomAttributes(false)
        .OfType<VersionedEventAttribute>()
        .FirstOrDefault() ?? new VersionedEventAttribute(t.Namespace ?? "Oink", t.Name);

        return attr.FullVersionedName;
    }));

    private static Dictionary<string, Type> NameToType => _nameToType.Value;

    private static readonly Lazy<IEnumerable<Type>> _eventTypes = new(() => AppDomain.CurrentDomain.GetAssemblies()
        .Where(x => !x.IsDynamic && x.GetName().Name?.StartsWith("Microsoft") != true && x.GetName().Name?.StartsWith("System") != true)
        .SelectMany(x => x.DefinedTypes.Where(IsDomainEventType)));
    private static IEnumerable<Type> EventTypes => _eventTypes.Value;

    private static bool IsDomainEventType(Type type) => !type.IsGenericTypeDefinition && typeof(IDomainEvent).IsAssignableFrom(type);

    public static Type GetEventDataType(string eventTypeName)
    {
        return NameToType.TryGetValue(eventTypeName, out var eventType) ? eventType : throw new InvalidOperationException($"Could not find type for event {eventTypeName}.");
    }
}
