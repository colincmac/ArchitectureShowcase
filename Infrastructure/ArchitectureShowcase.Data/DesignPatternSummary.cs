namespace ArchitectureShowcase.Data;
public class Document
{

    public string? Uid { get; set; }

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? AuthorAlias { get; set; }
    public string? Markdown { get; set; }

    public List<string> Tags { get; set; }
    = new List<string>();
    public string? ETag { get; set; }

    public override int GetHashCode()
    {
        return Uid.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return obj is Document document && document.Uid == Uid;
    }
}
