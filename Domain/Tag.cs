using ArchitectureShowcase.Core.Domain;
using ArchitectureShowcase.Domain.Seedwork;

namespace ArchitectureShowcase.Domain;
public class Tag : Entity, IDocumentSummaries
{
    public Tag(string tagName, List<DocumentSummary> documents)
    {
        TagName = tagName;
        Documents = documents;
    }

    public override string Id => TagName;
    public string TagName { get; set; }
    public List<DocumentSummary> Documents { get; set; }
}
