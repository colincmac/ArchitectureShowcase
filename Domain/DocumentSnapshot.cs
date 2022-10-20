using ArchitectureShowcase.Core.Domain;
using System.Text.Json;

namespace ArchitectureShowcase.Domain;
public class DocumentSnapshot : Entity
{

    public DocumentSnapshot(Document document)
    {
        Id = Guid.NewGuid().ToString();
        Uid = document.Id;
        Document = JsonSerializer.Serialize(document);
        Timestamp = DateTimeOffset.UtcNow;
    }
    public override string Id { get; protected set; }

    /// <summary>
    /// Gets or sets the identifier of the document.
    /// </summary>
    public string Uid { get; }

    /// <summary>
    /// Gets or sets the timestamp of the audit.
    /// </summary>
    public DateTimeOffset Timestamp { get; }

    /// <summary>
    /// Gets or sets the JSON serialized snapshot.
    /// </summary>
    public string Document { get; }

    /// <summary>
    /// Deserializes the snapshot.
    /// </summary>
    /// <returns>The <see cref="Document"/> snapshot.</returns>
    public Document? TryGetDocumentSnapshot() =>
        JsonSerializer.Deserialize<Document>(Document);

}
