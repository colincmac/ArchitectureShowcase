namespace ArchitectureShowcase.Domain.Commands;
public record RemoveTagsFromDocument(Guid Id, List<string> Tags);
