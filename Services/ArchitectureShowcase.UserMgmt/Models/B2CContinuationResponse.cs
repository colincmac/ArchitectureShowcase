using Newtonsoft.Json;
using System;

namespace ArchitectureShowcase.UserMgmt.Models;
public class B2CContinuationResponse
{
    public string Version { get; set; } = "1.0.0";
    public string Action { get; set; } = "Continue";

    [JsonProperty("extension_d4166fb1-5fa5-4705-baf2-a6e3c8517cde_archAppProfileId")]
    public Guid UserId { get; set; }
}
