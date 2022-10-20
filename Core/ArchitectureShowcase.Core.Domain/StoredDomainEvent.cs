using System.Text.Json.Serialization;
namespace ArchitectureShowcase.Core.Domain;

// Reference: https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/core/Azure.Core/src/Messaging/CloudEvent.cs
[JsonConverter(typeof(StoredDomainEventConverter))]
public class StoredDomainEvent
{

    public StoredDomainEvent(string subject, string type, string source, object jsonSerializableData, Type? dataSerializationType = default)
    {
        Id = Guid.NewGuid().ToString();
        Subject = subject;
        Type = type;
        Source = source;
        Data = new BinaryData(jsonSerializableData, type: dataSerializationType ?? jsonSerializableData?.GetType());
    }

    internal StoredDomainEvent() { }
    /// <summary>
    /// Gets or sets an identifier for the event. The combination of <see cref="Id"/> and <see cref="Source"/> must be unique for each distinct event.
    /// If not explicitly set, this will default to a <see cref="Guid"/>.
    /// </summary>
    public string Id { get; set; }


    /// <summary>
    /// Subject of the event in the context of the event producer (identified by source).
    /// In Cosmos, acts as the Partition Key.
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// Type of event related to the originating occurrence. Example: "Oink.UserProfile.Created"
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Identifies the context in which an event happened. The combination of <see cref="Id"/> and <see cref="Source"/> must be unique for each distinct event.
    /// </summary>
    public string Source { get; set; }

    /// <summary>
    /// Gets or sets the time (in UTC) the event was generated, in RFC3339 format.
    /// If not explicitly set, this will default to the time that the event is constructed.
    /// </summary>
    public DateTimeOffset Time { get; set; } = DateTimeOffset.UtcNow;

    public BinaryData Data { get; set; }
}
