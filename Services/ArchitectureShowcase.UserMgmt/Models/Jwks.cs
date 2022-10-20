using Newtonsoft.Json;
using System.Collections.Generic;

namespace ArchitectureShowcase.UserMgmt.Models;

public class Jwks
{
    [JsonProperty("keys")]
    public ICollection<JwksKey> Keys { get; set; }
}
