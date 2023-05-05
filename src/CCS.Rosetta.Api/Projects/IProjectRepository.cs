namespace CCS.Rosetta.Api.Projects;

public interface IProjectRepository
{
    Task Add(Project project);
}