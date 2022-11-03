using Azure.Messaging;
using System.Text.Json.Serialization;

namespace ArchitectureShowcase.Core.Azure.Cosmos;

/*
 * CloudEvents have a strict schema and Cosmos serialization adds properties that will fail validation checks.
 * This wraps the event in an opionated way to avoid issues without providing our own JsonConverters.
 */
public class CloudEventWrapper
{
    [JsonConstructor]
    public CloudEventWrapper(string id, string partitionKey, CloudEvent @event)
    {
        Event = @event;
        PartitionKey = partitionKey;
        Id = id;
    }

    public string Id { get; set; }
    public string PartitionKey { get; set; }
    public CloudEvent Event { get; set; }


    public static CloudEventWrapper FromCloudEvent(CloudEvent cloudEvent) => new(cloudEvent.Id, cloudEvent.Subject ?? cloudEvent.Type, cloudEvent);
}
