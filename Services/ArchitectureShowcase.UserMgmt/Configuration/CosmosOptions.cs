using System.Collections.Generic;

namespace ArchitectureShowcase.UserMgmt.Configuration;

public class CosmosOptions
{
    public string DatabaseName { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
    public bool ReadOnly { get; set; } = true;
    public string AccountName { get; set; } = string.Empty;
    public string SubscriptionId { get; set; } = string.Empty;
    public string ResourceGroup { get; set; } = string.Empty;
    public ICollection<string> Containers { get; set; } = new List<string>();  // Used for Health Checks

    // Optional
    public string ConnectionKey { get; set; }
    public string PrimaryRegion { get; set; }
}
