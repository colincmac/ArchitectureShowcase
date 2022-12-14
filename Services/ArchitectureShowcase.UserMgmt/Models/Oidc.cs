using Newtonsoft.Json;
using System.Collections.Generic;

namespace ArchitectureShowcase.UserMgmt.Models;

public class Oidc
{
    [JsonProperty("issuer")]
    public string Issuer { get; set; }

    [JsonProperty("jwks_uri")]
    public string JwksUri { get; set; }

    [JsonProperty("id_token_signing_alg_values_supported")]
    public ICollection<string> IdTokenSigningAlgValuesSupported { get; set; }
}
