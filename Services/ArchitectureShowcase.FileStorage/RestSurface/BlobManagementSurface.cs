using ArchitectureShowcase.Core.Common;
using ArchitectureShowcase.FileStorage.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ArchitectureShowcase.FileStorage.RestSurface;

public class BlobManagementSurface
{

    // https://learn.microsoft.com/en-us/azure/storage/blobs/storage-blob-user-delegation-sas-create-dotnet#get-a-user-delegation-sas-for-a-blob
    [FunctionName(nameof(GenerateBlobSasUri))]
    //[OpenApiOperation(operationId: nameof(GenerateBlobSasToken), tags: new[] { "file" })]
    //[OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    //[OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
    //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public async Task<OkObjectResult> GenerateBlobSasUri(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "{solutionId:guid}/files/{fileName:alpha}.{fileType:alpha}/generateSasUri")] HttpRequest req,
        [Blob("solutionFiles/{solutionId}/archfiles/{fileName}.{fileType}", FileAccess.Write, Connection = "ArchFiles")] BlobClient blobClient
        )
    {
        Azure.Response<GetBlobTagResult> tags = await blobClient.GetTagsAsync();
        Azure.Response<BlobProperties> properties = await blobClient.GetPropertiesAsync();
        BlobServiceClient blobServiceClient = blobClient.GetParentBlobContainerClient().GetParentBlobServiceClient();
        Azure.Response<UserDelegationKey> userDelegationKey = await blobServiceClient.GetUserDelegationKeyAsync(DateTimeOffset.UtcNow,
                                                      DateTimeOffset.UtcNow.AddSeconds(30));
        DateTimeOffset expiresOn = DateTimeOffset.UtcNow.AddMinutes(30);
        Uri blobUri = CreateBlobSasUriFromUserDelegationKey(userDelegationKey, blobClient, blobServiceClient.AccountName, DateTimeOffset.UtcNow, expiresOn);
        var result = new BlobSasUriResponse(blobUri, expiresOn, new Dictionary<string, string>(tags.Value.Tags), ArchFileMetadata.FromBlobProperties(properties));
        return new OkObjectResult(new ResponseEnvelope<BlobSasUriResponse>(result));
    }

    [FunctionName(nameof(ListFilesForSolution))]
    public async Task<OkObjectResult> ListFilesForSolution(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "{solutionId:guid}/files")] HttpRequest req,
    [Blob("solutionFiles/{solutionId}", FileAccess.Read, Connection = "ArchFiles")] IEnumerable<BlobClient> blobs
    )
    {
        if (!blobs.Any()) return new OkObjectResult(Enumerable.Empty<BlobSasUriResponse>());
        BlobServiceClient blobServiceClient = blobs.First().GetParentBlobContainerClient().GetParentBlobServiceClient();
        Azure.Response<UserDelegationKey> userDelegationKey = await blobServiceClient.GetUserDelegationKeyAsync(DateTimeOffset.UtcNow,
                                              DateTimeOffset.UtcNow.AddMinutes(5));
        DateTimeOffset startsOn = DateTimeOffset.UtcNow;
        DateTimeOffset expiresOn = DateTimeOffset.UtcNow.AddMinutes(30);

        var filesAsyncList = blobs.Select(async client =>
        {
            Uri sasUri = CreateBlobSasUriFromUserDelegationKey(userDelegationKey, client, blobServiceClient.AccountName, DateTimeOffset.UtcNow, expiresOn);
            Azure.Response<GetBlobTagResult> tags = await client.GetTagsAsync();
            Azure.Response<BlobProperties> properties = await client.GetPropertiesAsync();

            return new BlobSasUriResponse(
            sasUri,
            expiresOn,
            new Dictionary<string, string>(tags.Value.Tags),
            ArchFileMetadata.FromBlobProperties(properties)
            );
        }).ToList();

        BlobSasUriResponse[] fileList = await Task.WhenAll(filesAsyncList);
        return new OkObjectResult(new ResponseEnvelope<IEnumerable<BlobSasUriResponse>>(fileList));
    }



    public static Uri CreateBlobSasUriFromUserDelegationKey(UserDelegationKey userDelegationKey, BlobClient blobClient, string accountName, DateTimeOffset startTime, DateTimeOffset expirationTime)
    {
        var sasBuilder = new BlobSasBuilder()
        {
            BlobContainerName = blobClient.BlobContainerName,
            BlobName = blobClient.Name,
            Resource = "b",
            StartsOn = startTime,
            ExpiresOn = expirationTime
        };
        sasBuilder.SetPermissions(BlobSasPermissions.Read |
                              BlobSasPermissions.Write);

        var blobUriBuilder = new BlobUriBuilder(blobClient.Uri)
        {
            Sas = sasBuilder.ToSasQueryParameters(userDelegationKey, accountName)
        };

        return blobUriBuilder.ToUri();
    }

}

