using System;

namespace ArchitectureShowcase.ArchitectureStore.HttpSurface;
public record CreateSolutionRequest(string Title, string Description, string AuthorAlias, string Summary, string Markdown);
public record PublishSolutionRequest(Guid SolutionId);
//public record AddTagToSolutionRequest();
//public record RemoveTagFromSolutionRequest();
