using ArchitectureShowcase.UserMgmt.Configuration;
using ArchitectureShowcase.UserMgmt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;


namespace ArchitectureShowcase.UserMgmt;

public class OidcSurface
{
    private readonly B2CConfiguration _b2CConfig;
    private readonly string _hostName;

    public OidcSurface(IOptions<B2CConfiguration> b2CConfig)
    {
        _b2CConfig = b2CConfig.Value;
        _hostName = Environment.GetEnvironmentVariable("OidcIssuerHostname") ?? throw new InvalidOperationException("Could not get hostname from environment");
    }

    [FunctionName(nameof(GetOidcConfiguration))]
    public IActionResult GetOidcConfiguration([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = ".well-known/openid-configuration")] HttpRequest req, ILogger log)
    {

        Microsoft.IdentityModel.Tokens.X509SigningCredentials credential = AzureIdentityHelpers.GetOidcSigningCredentials(log);

        return new OkObjectResult(new Oidc
        {
            // The issuer name is the application root path
            Issuer = $"https://{_hostName}/",

            // Include the absolute URL to JWKs endpoint
            JwksUri = $"https://{_hostName}/api/.well-known/keys",

            // Include the supported signing algorithms
            IdTokenSigningAlgValuesSupported = new[] { credential.Algorithm },
        });
    }

    [FunctionName(nameof(GetJwksDocument))]
    public IActionResult GetJwksDocument([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = ".well-known/keys")] HttpRequest req, ILogger log)
    {

        Microsoft.IdentityModel.Tokens.X509SigningCredentials credential = AzureIdentityHelpers.GetOidcSigningCredentials(log);

        return new OkObjectResult(new Jwks
        {
            Keys = new[] { JwksKey.FromSigningCredentials(credential) }
        });
    }
}


