namespace ArchitectureShowcase.Domain.Commands;

public record UpdateDocument(Guid Id, string? Title, string? Description, string? AuthorAlias, string? Summary, string? Markdown, Uri? ThumbnailUri, Uri? DiagramUri);
