using ArchitectureShowcase.UserMgmt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;

namespace ArchitectureShowcase.UserMgmt.B2cApiConnectors;

public class SignUpEnrichmentHttpSurface
{

    [FunctionName(nameof(OnboardUser))]
    public IActionResult OnboardUser([HttpTrigger(AuthorizationLevel.Anonymous, "post")] B2CApiConnectorInput input, ILogger log)
    {


        return new OkObjectResult(new { archAppProfileId = Guid.NewGuid().ToString() });
    }
}
