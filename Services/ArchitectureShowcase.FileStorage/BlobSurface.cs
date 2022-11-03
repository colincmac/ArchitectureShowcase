using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ArchitectureShowcase.FileManagement;
public class BlobSurface
{
    [FunctionName(nameof(HandleDocumentFileUpserted))]
    public async Task HandleDocumentFileUpserted(
    [BlobTrigger("solutionfiles/{solutionId}/archfiles/{fileName}.{fileType}", Source = BlobTriggerSource.EventGrid, Connection = "ArchFiles")] BlobClient blob,
    string solutionId,
    string fileName,
    string fileType,
    ILogger log
    )
    {
        Response<BlobProperties> blobProperties = await blob.GetPropertiesAsync();
    }

    [FunctionName(nameof(HandleUpdatedThumbnail))]
    public async Task HandleUpdatedThumbnail(
    [BlobTrigger("solutionfiles/{solutionId}/thumbs/{fileName}.png", Source = BlobTriggerSource.EventGrid, Connection = "ArchFiles")] BlobClient blob,
    string solutionId,
    string fileName,
    string fileType,
    ILogger log
    )
    {
        Response<BlobProperties> blobProperties = await blob.GetPropertiesAsync();
    }
}
