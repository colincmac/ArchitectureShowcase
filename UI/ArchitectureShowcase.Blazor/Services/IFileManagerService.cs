namespace ArchitectureShowcase.Blazor.Services;

public interface IFileManagerService
{
    Task UpsertSolutionFile(Guid solutionId);
    Task UpdateSolutionFileMetadata(Guid solutionId, Guid fileId);
}
