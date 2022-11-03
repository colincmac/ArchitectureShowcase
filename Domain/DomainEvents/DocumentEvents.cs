using ArchitectureShowcase.Core.Domain;
using ArchitectureShowcase.Core.Domain.Contracts;
using ArchitectureShowcase.Domain.Seedwork;

namespace ArchitectureShowcase.Domain.DomainEvents;

[VersionedEvent("ArchitectureShowcase.SolutionsStore", nameof(DocumentCreated))]
public record DocumentCreated(string DocumentType, string Title, string Description, string? AuthorAlias, string? Summary, string? Markdown, Uri? ThumbnailUri, Uri? DiagramUri) : IDomainEvent;

[VersionedEvent("ArchitectureShowcase.SolutionsStore", nameof(DocumentUpdated))]
public record DocumentUpdated(string? Title, string? Description, string? AuthorAlias, string? Summary, string? Markdown, Uri? ThumbnailUri, Uri? DiagramUri) : IDomainEvent;

[VersionedEvent("ArchitectureShowcase.SolutionsStore", nameof(DocumentDeleted))]
public record DocumentDeleted() : IDomainEvent;

[VersionedEvent("ArchitectureShowcase.SolutionsStore", nameof(DocumentStatusChanged))]
public record DocumentStatusChanged(DocumentStatusEnum newStatus) : IDomainEvent;

[VersionedEvent("ArchitectureShowcase.SolutionsStore", nameof(DocumentTagsAdded))]
public record DocumentTagsAdded(List<string> Tags) : IDomainEvent;

[VersionedEvent("ArchitectureShowcase.SolutionsStore", nameof(DocumentTagsRemoved))]
public record DocumentTagsRemoved(List<string> Tags) : IDomainEvent;
