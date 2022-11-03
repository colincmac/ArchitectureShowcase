using ArchitectureShowcase.Core.Domain;
using ArchitectureShowcase.Core.Domain.Contracts;
using ArchitectureShowcase.Domain.Commands;
using ArchitectureShowcase.Domain.DomainEvents;
using ArchitectureShowcase.Domain.Seedwork;

namespace ArchitectureShowcase.Domain;
public class Document : AggregateRoot
{
    public string Type { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string? AuthorAlias { get; private set; } = string.Empty;
    public string? Summary { get; private set; } = string.Empty;
    public string? Markdown { get; private set; } = string.Empty;
    public Uri? ThumbnailUri { get; private set; }
    public Uri? DiagramUri { get; private set; }
    public DocumentStatusEnum Status { get; private set; } = DocumentStatusEnum.Draft;
    public bool IsActive => Status != DocumentStatusEnum.Deleted;
    public HashSet<string> Tags { get; } = new HashSet<string>();
    public List<string> Files { get; } = new List<string>();

    protected Document(Guid id) : base()
    {
        Id = id.ToString();
    }

    public Document(IEnumerable<IDomainEvent> eventItems) : base(eventItems)
    {
    }

    #region CommandHandlers
    public static Document Create(CreateDocument command)
    {
        var newSolution = new Document(command.Id);
        newSolution.Apply(new DocumentCreated(command.DocumentType, command.Title, command.Description, command.AuthorAlias, command.Summary, command.Markdown, command.ThumbnailUri, command.DiagramUri));
        return newSolution;
    }

    public Document Delete(DeleteDocument command)
    {
        Apply(new DocumentStatusChanged(DocumentStatusEnum.Deleted));
        return this;
    }

    public Document Publish(PublishDocument command)
    {
        Apply(new DocumentStatusChanged(DocumentStatusEnum.Deleted));
        return this;
    }

    public Document Update(UpdateDocument command)
    {
        Apply(new DocumentUpdated(command.Title, command.Description, command.AuthorAlias, command.Summary, command.Markdown, command.ThumbnailUri, command.DiagramUri));
        return this;
    }

    public Document AddTags(AddTagsToDocument command)
    {
        var addedTags = command.Tags.Select(x => x.Trim()).ToList();
        Apply(new DocumentTagsAdded(addedTags));
        return this;
    }

    public Document RemoveTags(RemoveTagsFromDocument command)
    {
        Apply(new DocumentTagsRemoved(command.Tags));
        return this;
    }
    #endregion

    #region EventHandlers

    public void On(DocumentCreated eventItem)
    {
        Type = eventItem.DocumentType;
        Title = eventItem.Title;
        Description = eventItem.Description;
        AuthorAlias = eventItem.AuthorAlias;
        Summary = eventItem.Summary;
        Markdown = eventItem.Markdown;
        ThumbnailUri = eventItem.ThumbnailUri;
        DiagramUri = eventItem.DiagramUri;
    }

    public void On(DocumentUpdated eventItem)
    {
        Title = eventItem.Title ?? Title;
        Description = eventItem.Description ?? Description;
        AuthorAlias = eventItem.AuthorAlias ?? AuthorAlias;
        Summary = eventItem.Summary ?? Summary;
        Markdown = eventItem.Markdown ?? Markdown;
        ThumbnailUri = eventItem.ThumbnailUri ?? ThumbnailUri;
        DiagramUri = eventItem.DiagramUri ?? DiagramUri;
    }

    public void On(DocumentStatusChanged eventItem)
    {
        Status = eventItem.newStatus;
    }

    public void On(DocumentTagsAdded eventItem)
    {
        Tags.UnionWith(eventItem.Tags);
    }

    public void On(DocumentTagsRemoved eventItem)
    {
        Tags.ExceptWith(eventItem.Tags);
    }
    #endregion
}
