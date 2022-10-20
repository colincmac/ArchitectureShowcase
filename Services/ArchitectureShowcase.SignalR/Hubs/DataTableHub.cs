using Microsoft.Extensions.Logging;

namespace ArchitectureShowcase.SignalR.Hubs;
public class DataTableHub : ServerlessHub
{

    [FunctionName("negotiate")]
    public SignalRConnectionInfo Negotiate([HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req, ClaimsPrincipal principal)
    {
        // TODO: Integrate with App Service Auth
        // https://learn.microsoft.com/en-us/azure/azure-signalr/signalr-concept-serverless-development-config#azure-functions-configuration
        return Negotiate(req.Headers["x-ms-client-principal-id"]);
    }

    [FunctionName(nameof(OnConnected))]
    public Task OnConnected([SignalRTrigger] InvocationContext invocationContext, ILogger logger)
    {
        logger.LogInformation($"{invocationContext.ConnectionId} has connected");
        return Task.CompletedTask;
    }

    [FunctionName(nameof(JoinUserToGroup))]
    public async Task JoinUserToGroup([SignalRTrigger] InvocationContext invocationContext, string userName, string groupName, ILogger logger)
    {
        logger.LogInformation($"{invocationContext.ConnectionId} joining {userName} {groupName}");

        await UserGroups.AddToGroupAsync(userName, groupName);
    }

}
