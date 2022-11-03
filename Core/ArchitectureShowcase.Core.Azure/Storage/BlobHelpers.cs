using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;

namespace ArchitectureShowcase.Core.Azure.Storage;
public static class BlobHelpers
{
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
