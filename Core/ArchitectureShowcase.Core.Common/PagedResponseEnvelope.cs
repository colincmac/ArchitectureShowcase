namespace ArchitectureShowcase.Core.Common;
public class PagedResponseEnvelope<TData> where TData : class
{
    public ICollection<TData> Data { get; set; } = new List<TData>();
    public PageInfo PageInfo { get; set; }

    public PagedResponseEnvelope(ICollection<TData> data, PageInfo pageInfo)
    {
        Data = data;
        PageInfo = pageInfo;
    }
}
