using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.SignalR.Client;

namespace ArchitectureShowcase.Blazor.Helpers;
public static class SignalRExtensions
{
    public static async ValueTask AddUserToGroup(this HubConnection hub, string groupName)
    {
        await hub.SendAsync(SignalRConstants.AddUserToGroupMethodName, groupName);
    }

    public static HubConnection CreateDefaultHub(IAccessTokenProvider tokenProvider)
    {
        return new HubConnectionBuilder()
            .WithUrl(new Uri(SignalRConstants.SignalRNegotiateUrl), options =>
        {
            options.HttpMessageHandlerFactory = innerHandler =>
                new IncludeRequestCredentialsMessageHandler { InnerHandler = innerHandler };
            options.AccessTokenProvider = async () =>
            {
                AccessTokenResult tokenResult = await tokenProvider.RequestAccessToken();
                // TODO: throw error if no token
                return tokenResult.TryGetToken(out AccessToken? token) ? token.Value : null;
            };

        })
        .Build();

    }
}