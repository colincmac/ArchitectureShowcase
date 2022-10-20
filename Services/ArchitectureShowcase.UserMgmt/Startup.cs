using ArchitectureShowcase.UserMgmt;
using ArchitectureShowcase.UserMgmt.Configuration;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace ArchitectureShowcase.UserMgmt;

public class Startup : FunctionsStartup
{
    private IConfiguration _configuration;

    public override void Configure(IFunctionsHostBuilder builder)
    {
        ConfigureSettings();
        ConfigureDependencies(builder);
    }

    private void ConfigureSettings()
    {
        var config = new ConfigurationBuilder();
        _configuration = config.Build();
    }

    private void ConfigureDependencies(IFunctionsHostBuilder builder)
    {
        _ = builder.Services.AddOptions<B2CConfiguration>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection("AzureADB2C").Bind(settings);
            });

        //builder.Services.AddOptions<B2CClientUserConfiguration>()
        //    .Configure<IConfiguration>((settings, configuration) =>
        //    {
        //        configuration.GetSection("ClientUserFlow").Bind(settings);
        //    });
        //_ = builder.Services.AddDurableClientFactory();
    }
}