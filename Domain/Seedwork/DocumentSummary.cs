namespace ArchitectureShowcase.Domain.Seedwork;
public class DocumentSummary
{
    public DocumentSummary(Document document)
    {
        Id = document.Id;
        Title = document.Title;
        Description = document.Description;
        DocumentType = nameof(Document);
        ThumbnailUri = document.ThumbnailUri;
    }

    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string DocumentType { get; set; }
    public Uri? ThumbnailUri { get; set; }
}
