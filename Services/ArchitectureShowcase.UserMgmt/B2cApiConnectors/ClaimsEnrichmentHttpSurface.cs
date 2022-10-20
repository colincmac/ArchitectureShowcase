namespace ArchitectureShowcase.UserMgmt.B2cApiConnectors;

//public class ClaimsEnrichmentHttpSurface
//{
//    private readonly B2CConfiguration _b2CConfig;

//    public ClaimsEnrichmentHttpSurface(IOptions<B2CConfiguration> b2CConfig)
//    {
//        _b2CConfig = b2CConfig.Value;
//    }

//    [FunctionName(nameof(EnrichClaimsWithGroupMembership))]
//    public async Task<IActionResult> EnrichClaimsWithGroupMembership([HttpTrigger(AuthorizationLevel.Anonymous, "post")] B2CInputClaims claims, ILogger log)
//    {
//        GraphServiceClient b2CClient = AzureIdentityHelpers.CreateB2CGraphClient(_b2CConfig);
//        IDirectoryObjectGetMemberGroupsCollectionPage userGroupRequest = await b2CClient.Users[claims.ObjectId]
//            .GetMemberGroups(true)
//            .Request()
//            .PostAsync();

//        var userGroups = new List<string>();
//        var pageIterator = PageIterator<string>
//            .CreatePageIterator(
//                b2CClient,
//                userGroupRequest,
//                // Callback executed for each item in
//                // the collection
//                m =>
//                {
//                    userGroups.Add(m);
//                    return true;
//                }
//            );
//        await pageIterator.IterateAsync();
//        while (pageIterator.State != PagingState.Complete)
//        {
//            await pageIterator.ResumeAsync();
//        }

//        return new OkObjectResult(new { Groups = userGroups });
//    }


//}
