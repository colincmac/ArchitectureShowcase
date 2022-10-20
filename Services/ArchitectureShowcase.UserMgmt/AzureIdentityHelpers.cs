using ArchitectureShowcase.UserMgmt.Configuration;
using Azure.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;


namespace ArchitectureShowcase.UserMgmt;

public static class AzureIdentityHelpers
{
    private static readonly Lazy<DefaultAzureCredential> AzureDefaultCredentialLazy =
        new(
            () =>
                new DefaultAzureCredential(
                    Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development"),
            LazyThreadSafetyMode.PublicationOnly);



    public static DefaultAzureCredential AzureDefaultCredential => AzureDefaultCredentialLazy.Value;

    public static GraphServiceClient CreateB2CGraphClient(B2CConfiguration config)
    {
        var credential = new ClientSecretCredential($"{config.TenantId}.onmicrosoft.com", config.ClientId, config.ClientSecret);
        return new GraphServiceClient(credential);
    }

    public static string GetB2CCustomAttributeClaim(string claimName, string b2CExtensionAppId)
    {
        return $"extension_{b2CExtensionAppId.Replace("-", string.Empty)}_{claimName}";
    }

    public static X509SigningCredentials GetOidcSigningCredentials(ILogger log = null)
    {
        string oidcCertString = Environment.GetEnvironmentVariable("OidcCertificate") ?? throw new InvalidOperationException("Could not load certificate from configuration.");

        byte[] certBytes = Convert.FromBase64String(oidcCertString);

        var cert = new X509Certificate2(certBytes, string.Empty, X509KeyStorageFlags.PersistKeySet);

        return new X509SigningCredentials(cert);
    }

}