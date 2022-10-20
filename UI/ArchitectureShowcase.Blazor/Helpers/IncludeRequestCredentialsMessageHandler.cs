using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace ArchitectureShowcase.Blazor.Helpers;

// Reference https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/signalr?view=aspnetcore-6.0#signalr-cross-origin-negotiation-for-authentication-blazor-webassembly
public class IncludeRequestCredentialsMessageHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
    HttpRequestMessage request, CancellationToken cancellationToken)
    {
        _ = request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        return base.SendAsync(request, cancellationToken);
    }
}
