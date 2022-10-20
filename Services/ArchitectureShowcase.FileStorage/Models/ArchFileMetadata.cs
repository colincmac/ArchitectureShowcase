using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;

namespace ArchitectureShowcase.FileStorage.Models;
public class ArchFileMetadata
{
    public DateTimeOffset LastModified { get; set; }

    public DateTimeOffset CreatedOn { get; set; }

    public IDictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();

    public string ContentType { get; set; } = string.Empty;

    public static ArchFileMetadata FromBlobProperties(BlobProperties blobProperties)
    {
        return new ArchFileMetadata
        {
            LastModified = blobProperties.LastModified,
            ContentType = blobProperties.ContentType,
            Metadata = blobProperties.Metadata,
            CreatedOn = blobProperties.CreatedOn
        };
    }
}
