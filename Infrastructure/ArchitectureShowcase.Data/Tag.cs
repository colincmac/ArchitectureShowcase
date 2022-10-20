namespace ArchitectureShowcase.Data;
public class Tag
{
    public string? TagName { get; set; }
    public List<ArchitectureSolution> Solutions { get; set; } = new List<ArchitectureSolution>();
    public string? Etag { get; set; }

    public override int GetHashCode()
    {
        return TagName.GetHashCode();
    }

    /// <summary>
    /// Implements equality.
    /// </summary>
    /// <param name="obj">The object to compare to.</param>
    /// <returns>A value indicating whether the tag names match.</returns>
    public override bool Equals(object obj)
    {
        return obj is Tag tag && tag.TagName == TagName;
    }
}
