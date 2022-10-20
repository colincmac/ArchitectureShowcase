using Azure.Messaging;
using System.Text.Json.Serialization;

namespace ArchitectureShowcase.Core.Azure.Cosmos;
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
