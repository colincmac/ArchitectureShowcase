using ArchitectureShowcase.Core.Azure.Cosmos;
using ArchitectureShowcase.Core.Domain.Contracts;
using ArchitectureShowcase.Core.Domain.Extensions;
using ArchitectureShowcase.Domain;
using ArchitectureShowcase.Domain.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace ArchitectureShowcase.ArchitectureStore.HttpSurface;

public static class ArchitectureStoreHttpSurface
{

    public const string EventSource = "ArchitectureShowcase.ArchitectureStore";

    [FunctionName(nameof(GetSolutionById))]
    public static Task<OkObjectResult> GetSolutionById(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "solutions/{id}")] HttpRequest req,
    [CosmosDB(databaseName: "architecture-showcase",
                containerName: "documentEvents",
                Connection = "ArchFilesCosmos",
                PartitionKey = "Document/{id}")] IEnumerable<CloudEventWrapper> documentEvents
)
    {
        IEnumerable<IDomainEvent> domainEvents = documentEvents.Select(x => x.Event.ToDomainEvent());
        var document = new Document(domainEvents);

        return Task.FromResult(new OkObjectResult(document));
    }

    [FunctionName(nameof(CreateSolution))]
    public static async Task<OkObjectResult> CreateSolution(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "solutions/create")] CreateDocument req,
        [CosmosDB(databaseName: "architecture-showcase",
            containerName: "metadata",
            Connection = "ArchFilesCosmos")] IAsyncCollector<Tag> updatedTags,
        [CosmosDB(databaseName: "architecture-showcase",
            containerName: "documentEvents",
            Connection = "ArchFilesCosmos")] IAsyncCollector<CloudEventWrapper> newDocumentEvents
    )
    {
        var newDocument = Document.Create(req);
        foreach (IDomainEvent domainEvent in newDocument.DomainEvents)
        {
            var cloudEvent = domainEvent.ToCloudEvent(EventSource, newDocument.GetGlobalIdentifier());
            var wrapper = CloudEventWrapper.FromCloudEvent(cloudEvent);
            await newDocumentEvents.AddAsync(wrapper);
        }

        return new OkObjectResult(newDocument);
    }

}
