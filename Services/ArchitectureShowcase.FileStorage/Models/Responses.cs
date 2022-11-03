using System;
using System.Collections.Generic;

namespace ArchitectureShowcase.FileStorage.Models;
public record BlobSasUriResponse(Uri SasUri, DateTimeOffset Expiration, Dictionary<string, string> BlobTags, ArchFileMetadata Properties);
