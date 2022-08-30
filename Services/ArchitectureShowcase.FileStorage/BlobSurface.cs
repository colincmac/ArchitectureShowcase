using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ArchitectureShowcase.FileStorage;
public class BlobSurface
{
    //https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/storage/Microsoft.Azure.WebJobs.Extensions.Storage.Blobs#examples
    // NOTE: If depoying with KEDA, the autoscaler does not create an instance per blob event.
    // Instead it scales based on how many blobs are currently in the container.
    [FunctionName(nameof(HandleBlobWithTrigger))]
    public async Task HandleBlobWithTrigger(
        [BlobTrigger("blobtriggerstaging/{name}", Connection = "TestBlob")] BlobClient blobClient,
        ILogger log)
    {
        var properties = await blobClient.GetPropertiesAsync();
        log.LogInformation($"C# Blob trigger function Processed blob\n Name:{blobClient.BlobContainerName} \n Size: {properties.Value.ContentLength} Bytes");
        var isDeleted = await blobClient.DeleteIfExistsAsync();
        log.LogInformation($"Blob successfully deleted: {isDeleted}");
    }

}
