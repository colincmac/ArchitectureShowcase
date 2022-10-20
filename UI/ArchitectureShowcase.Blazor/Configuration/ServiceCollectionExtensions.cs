using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ArchitectureShowcase.Blazor.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, WebAssemblyHostBuilder builder)
    {
        _ = services.AddBlazoredLocalStorage();
        _ = services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        //_ = builder.Services.AddHttpClient("WebAPI", client => client.BaseAddress = new Uri("https://localhost:7081/api"))
        //    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

        //_ = builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
        //    .CreateClient("WebAPI"));
        return services;
    }

    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, WebAssemblyHostBuilder builder)
    {
        _ = services.AddMsalAuthentication(options =>
        {
            builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
            options.ProviderOptions.DefaultAccessTokenScopes.Add("openid");
            options.ProviderOptions.DefaultAccessTokenScopes.Add("offline_access");
            //options.ProviderOptions.DefaultAccessTokenScopes
            //    .Add("https://graph.microsoft.com/User.Read");
        });

        return services;
    }
}
