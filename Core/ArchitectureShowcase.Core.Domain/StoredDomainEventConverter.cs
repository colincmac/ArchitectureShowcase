using System.Text.Json;
using System.Text.Json.Serialization;

namespace ArchitectureShowcase.Core.Domain;
public class StoredDomainEventConverter : JsonConverter<StoredDomainEvent>
{

    public override StoredDomainEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var requestDocument = JsonDocument.ParseValue(ref reader);
        return DeserializeStoredDomainEvent(requestDocument.RootElement);
    }

    internal static StoredDomainEvent DeserializeStoredDomainEvent(JsonElement element)
    {
        var eventItem = new StoredDomainEvent();

        foreach (JsonProperty property in element.EnumerateObject())
        {
            if (property.NameEquals("id"))
            {
                if (property.Value.ValueKind == JsonValueKind.Null) continue;

                eventItem.Id = property.Value.GetString()!;
            }
            else if (property.NameEquals("source"))
            {
                if (property.Value.ValueKind == JsonValueKind.Null) continue;

                eventItem.Source = property.Value.GetString()!;
            }
            else if (property.NameEquals("type"))
            {
                if (property.Value.ValueKind == JsonValueKind.Null) continue;

                eventItem.Type = property.Value.GetString()!;
            }
            else if (property.NameEquals("time"))
            {
                if (property.Value.ValueKind == JsonValueKind.Null) continue;

                eventItem.Time = property.Value.GetDateTimeOffset();
            }
            else if (property.NameEquals("subject"))
            {
                if (property.Value.ValueKind == JsonValueKind.Null) continue;

                eventItem.Subject = property.Value.GetString()!;
            }
            else if (property.NameEquals("data"))
            {
                eventItem.Data = new BinaryData(property.Value)!;
            }
        }

        return eventItem;
    }

    public override void Write(Utf8JsonWriter writer, StoredDomainEvent value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WritePropertyName("id");
        writer.WriteStringValue(value.Id);

        writer.WritePropertyName("source");
        writer.WriteStringValue(value.Source);

        writer.WritePropertyName("type");
        writer.WriteStringValue(value.Type);

        writer.WritePropertyName("subject");
        writer.WriteStringValue(value.Subject);

        writer.WritePropertyName("time");
        writer.WriteStringValue(value.Time);

        if (value.Data != null)
        {
            using JsonDocument doc = JsonDocument.Parse(value.Data.ToMemory());
            writer.WritePropertyName("data");
            doc.RootElement.WriteTo(writer);
        }

        writer.WriteEndObject();

    }
}
