using ArchitectureShowcase.ArchitectureStore;
using ArchitectureShowcase.Core.Azure.Cosmos;
using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;

[assembly: FunctionsStartup(typeof(Startup))]
namespace ArchitectureShowcase.ArchitectureStore;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        ConfigureServices(builder.Services);
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ICosmosDBSerializerFactory, CosmosSerializerFactory>();
    }

    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        var cs = Environment.GetEnvironmentVariable("AppConfigEndpoint");
        builder.ConfigurationBuilder.AddAzureAppConfiguration(opt => opt.Connect(new Uri(cs), new DefaultAzureCredential()));
    }

}

public class CosmosSerializerFactory : ICosmosDBSerializerFactory
{
    public CosmosSerializer CreateSerializer()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        return new CosmosSystemTextJsonSerializer(options);
    }
}
