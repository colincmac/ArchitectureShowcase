namespace ArchitectureShowcase.Core.Common;
public class ResponseEnvelope<TData> where TData : class
{
    public ResponseEnvelope(TData? data)
    {
        Data = data;
    }

    public TData? Data { get; set; }
}
