namespace ArchitectureShowcase.Domain.Commands;

public record AddTagsToDocument(Guid Id, List<string> Tags);
