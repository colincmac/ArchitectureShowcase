namespace ArchitectureShowcase.Data;
public class ArchitectureSolution
{
    public ArchitectureSolution()
    {

    }
    public ArchitectureSolution(DesignPattern document)
    {
        Uid = document.Uid;
        Title = document.Title;
    }

    public string? Uid { get; set; }
    public string? Title { get; set; }
    public string AuthorAlias { get; set; }
    public Uri? ThumbnailUri { get; set; }

    public override bool Equals(object obj)
    {
        return obj is ArchitectureSolution ds && ds.Uid == Uid;
    }

    public override int GetHashCode()
    {
        return Uid.GetHashCode();
    }
}
