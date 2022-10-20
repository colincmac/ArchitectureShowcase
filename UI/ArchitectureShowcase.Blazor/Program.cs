using ArchitectureShowcase.Blazor;
using ArchitectureShowcase.Blazor.Configuration;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddCoreServices(builder);
builder.Services.ConfigureAuthentication(builder);

await builder.Build().RunAsync();
