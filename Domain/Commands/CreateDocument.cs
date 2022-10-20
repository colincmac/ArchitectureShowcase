namespace ArchitectureShowcase.Domain.Commands;

public class CreateDocument
{
    public Guid Id { get; set; }
    public string DocumentType { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? AuthorAlias { get; set; }
    public string? Summary { get; set; }
    public string? Markdown { get; set; }
    public Uri? ThumbnailUri { get; set; }
    public Uri? DiagramUri { get; set; }
}