using ArchitectureShowcase.FileStorage.Models;
using System;
using System.Collections.Generic;

namespace ArchitectureShowcase.FileStorage.RestSurface;
public record BlobSasUriResponse(Uri SasUri, DateTimeOffset Expiration, Dictionary<string, string> BlobTags, ArchFileMetadata Properties);
